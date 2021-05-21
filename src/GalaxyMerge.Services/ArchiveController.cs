using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GalaxyMerge.Archestra.Abstractions;
using GalaxyMerge.Core;
using GalaxyMerge.Core.Utilities;
using GalaxyMerge.Primitives;

namespace GalaxyMerge.Services
{
    public class ArchiveController
    {
        private readonly IGalaxyRepositoryProvider _galaxyRepositoryProvider;
        private readonly List<SqlListener> _listeners;
        private const string ChangeLogTableName = "gobject_change_log";


        public ArchiveController(IGalaxyRepositoryProvider galaxyRepositoryProvider)
        {
            _galaxyRepositoryProvider = galaxyRepositoryProvider;
            _listeners = new List<SqlListener>();
        }

        public void Start()
        {
            InitializeListeners();

            foreach (var listener in _listeners)
                listener.Start();
        }

        public void Stop()
        {
            foreach (var listener in _listeners)
                listener.Stop();
            
            _listeners.Clear();
        }

        private void InitializeListeners()
        {
            var galaxies = _galaxyRepositoryProvider.GetAllServiceInstances();
            
            foreach (var galaxy in galaxies)
            {
                SetupServiceBroker(galaxy.Name);

                var connectionString = ConnectionStringBuilder.BuildGalaxyConnection(galaxy.Name);
                var listener = new SqlListener(connectionString, galaxy.Name, ChangeLogTableName);
                listener.TableChanged += OnChangeLogTableUpdated;
                
                _listeners.Add(listener);
            }
        }

        private static void SetupServiceBroker(string databaseName)
        {
            if (!SqlServiceBroker.IsUnique(databaseName))
            {
                SqlServiceBroker.New(databaseName);
                return;
            }
            
            if (!SqlServiceBroker.IsEnabled(databaseName))
                SqlServiceBroker.Enable(databaseName);
        }

        private void OnChangeLogTableUpdated(object sender, SqlListener.TableChangedEventArgs e)
        {
            if (!(sender is SqlListener listener)) return;
            if (!IsArchivable(e.Data, listener.DatabaseName)) return;

            var galaxyRepo = _galaxyRepositoryProvider.GetServiceInstance(listener.DatabaseName);
            var archiver = new ArchiveProcessor(galaxyRepo);
            
            var objectId = ExtractObjectId(e.Data);
            var operation = Enumeration.FromId<Operation>(ExtractOperationId(e.Data));
            archiver.Archive(objectId, operation);
        }
        
        private static bool IsArchivable(XContainer data, string galaxyName)
        {
            var settingValidator = new ArchiveSettingValidator(galaxyName);

            var objectId = ExtractObjectId(data);
            if (!settingValidator.HasValidInclusionOption(objectId)) return false;

            var operationId = ExtractOperationId(data);
            return settingValidator.IsValidArchiveTrigger(operationId);
        }
        
        private static int ExtractObjectId(XContainer data)
        {
            return Convert.ToInt32(data.Descendants("gobject_id").Select(x => x.Value).SingleOrDefault());
        }
        
        private static int ExtractOperationId(XContainer data)
        {
            return Convert.ToInt32(data.Descendants("operation_id").Select(x => x.Value).SingleOrDefault());
        }
    }
}