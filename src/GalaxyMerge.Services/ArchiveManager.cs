using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GalaxyMerge.Archestra.Abstractions;
using GalaxyMerge.Archestra.Entities;
using GalaxyMerge.Archive.Entities;
using GalaxyMerge.Archive.Repositories;
using GalaxyMerge.Common.Primitives;
using GalaxyMerge.Contracts;
using GalaxyMerge.Core.Extensions;

namespace GalaxyMerge.Services
{
    public class ArchiveManager : IArchiveService
    {
        private readonly IGalaxyRepositoryProvider _galaxyRepositoryProvider;
        private IGalaxyRepository _grSession;

        public ArchiveManager(IGalaxyRepositoryProvider galaxyRepositoryProvider)
        {
            _galaxyRepositoryProvider = galaxyRepositoryProvider;
        }
        
        public bool Connect(string galaxyName)
        {
            _grSession = _galaxyRepositoryProvider.GetClientInstance(galaxyName);
            return _grSession.Name == galaxyName && _grSession.Connected;
        }
        
        public ArchiveObject GetArchiveObject(int objectId)
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            return repo.GetObject(objectId);
        }

        public IEnumerable<ArchiveObject> GetArchiveObjects()
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            return repo.GetAllObjects();
        }

        public IEnumerable<ArchiveEntry> GetArchiveEntries()
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            return repo.GetAllEntries();
        }

        public GalaxyObject GetGalaxyObject(int objectId)
        {
            using var archiveRepository = new ArchiveRepository(_grSession.Name);

            var archiveObject = archiveRepository.GetObjectIncludeEntries(objectId);
            if (archiveObject == null) return null;

            if (archiveObject.Template == Template.Symbol)
                throw new InvalidOperationException("Cannot convert symbol to object");

            var latest = archiveObject.Entries.OrderByDescending(e => e.ArchivedOn).First();
            return MaterializeObject(latest);
        }
        
        public GalaxySymbol GetGalaxySymbol(int objectId)
        {
            using var archiveRepository = new ArchiveRepository(_grSession.Name);
            
            var archiveObject = archiveRepository.GetObjectIncludeEntries(objectId);
            if (archiveObject == null) return null;

            if (archiveObject.Template != Template.Symbol)
                throw new InvalidOperationException("Cannot convert symbol to object");

            var latest = archiveObject.Entries.OrderByDescending(e => e.ArchivedOn).FirstOrDefault();
            return MaterializeSymbol(latest);
        }

        public IEnumerable<EventSetting> GetEventSettings()
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            return repo.GetEventSettings();
        }

        public IEnumerable<InclusionSetting> GetInclusionSettings()
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            return repo.GetInclusionSettings();
        }

        public void AddObject(int objectId)
        {
            var archiver = new ArchiveProcessor(_grSession);
            archiver.Archive(objectId);
        }

        public void RemoveObject(int objectId)
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            repo.RemoveObject(objectId);
        }

        public void ArchiveObject(int objectId, bool force = false)
        {
            var archiver = new ArchiveProcessor(_grSession);
            archiver.Archive(objectId);
        }

        public void UpdateEventSetting(IEnumerable<EventSetting> eventSettings)
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            repo.UpdateEventSettings(eventSettings);
        }

        public void UpdateInclusionSetting(IEnumerable<InclusionSetting> inclusionSettings)
        {
            using var repo = new ArchiveRepository(_grSession.Name);
            repo.UpdateInclusionSettings(inclusionSettings);
        }
        
        private static GalaxyObject MaterializeObject(ArchiveEntry latest)
        {
            var xml = XElement.Load(new MemoryStream(latest.CompressedData.Decompress()));
            var galaxyObject = new GalaxyObject().FromXml(xml);
            return (GalaxyObject) galaxyObject;
        }

        private static GalaxySymbol MaterializeSymbol(ArchiveEntry latest)
        {
            var xml = XElement.Load(new MemoryStream(latest.CompressedData.Decompress()));
            var galaxyObject = new GalaxySymbol("").FromXml(xml); //todo figure out tagname here
            return (GalaxySymbol) galaxyObject;
        }
    }
}