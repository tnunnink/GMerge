using System;
using GServer.Archestra.Abstractions;
using GCommon.Data.Entities;
using GCommon.Utilities;
using NLog;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace GServer.Services
{
    public class ChangeLogMonitor : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private SqlTableDependency<ChangeLog> _changeLogDependency;
        private ArchivingPipeline _archivingPipeline;
        private const string ChangeLogTableName = "gobject_change_log";

        public ChangeLogMonitor(IGalaxyRepository galaxyRepository)
        {
            if (galaxyRepository == null)
                throw new ArgumentNullException(nameof(galaxyRepository), "galaxy repository can not be null");

            Logger.Trace("Initializing archive processors {Galaxy}",galaxyRepository.Name);
            
            SetupServiceBroker(galaxyRepository.Name);
            InitializeDependency(galaxyRepository.Name);
            InitializePipeline(galaxyRepository);
        }

        private static void SetupServiceBroker(string databaseName)
        {
            if (!SqlServiceBroker.IsUnique(databaseName))
            {
                Logger.Trace("Creating new Service Broker for galaxy database {Database}", databaseName);
                SqlServiceBroker.New(databaseName);
                return;
            }

            if (SqlServiceBroker.IsEnabled(databaseName)) return;

            Logger.Trace("Enabling Service Broker for galaxy database {Database}", databaseName);
            SqlServiceBroker.Enable(databaseName);
        }

        private void InitializeDependency(string databaseName)
        {
            var connectionString = DbStringBuilder.GalaxyString(databaseName);

            Logger.Trace("Initializing Table Dependency for galaxy database '{Database}'", databaseName);
            _changeLogDependency = new SqlTableDependency<ChangeLog>(connectionString, ChangeLogTableName,
                mapper: DataMapper.GetChangeLogMapper(), executeUserPermissionCheck: false);
            
            _changeLogDependency.OnChanged += OnChangeLogTableChanged;
            _changeLogDependency.OnError += OnChangeLogTableError;
            _changeLogDependency.OnStatusChanged += OnDependencyStatusChanges;

            Logger.Debug("Starting ChangeLog Table Dependency for '{Database}'", databaseName);
            _changeLogDependency.Start();
        }
        
        private void InitializePipeline(IGalaxyRepository galaxyRepository)
        {
            _archivingPipeline = new ArchivingPipeline(galaxyRepository);
            _archivingPipeline.Start();
        }

        private void OnChangeLogTableChanged(object sender, RecordChangedEventArgs<ChangeLog> e)
        {
            if (e.ChangeType != ChangeType.Insert) return;
            Logger.Trace("Change log event detected. Details: {@ChangeLog}", e.Entity);
            _archivingPipeline.Enqueue(e.Entity);
        }

        private void OnChangeLogTableError(object sender, ErrorEventArgs e)
        {
            Logger.Error(e.Error, e.Message);
            _changeLogDependency.Stop();
            _changeLogDependency.Start();
        }

        private static void OnDependencyStatusChanges(object sender, StatusChangedEventArgs e)
        {
            Logger.Trace("Depepndency status changed to {Status} for galaxy {Galaxy}", e.Status, e.Database);
            
            if (e.Status == TableDependencyStatus.Started)
                Logger.Debug("Change log table dependency started on {GalaxyName}", e.Database);
        }

        public void Dispose()
        {
            Logger.Trace("Stopping table dependency service");
            _changeLogDependency?.Stop();
        }
    }
}