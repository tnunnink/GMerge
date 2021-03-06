using System.IO;
using System.Security.Principal;
using GCommon.Archiving;
using GCommon.Core;
using GCommon.Core.Enumerations;
using GServer.Archestra;
using GTest.Core;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace GServer.Services.IntegrationTests
{
    [TestFixture]
    public class GalaxyArchiverTests
    {
        private ArchiveRepository _archiveRepo;
        private GalaxyRepository _galaxyRepo;
        private string _fileName;

        [SetUp]
        public void Setup()
        {
            _fileName = @$"\{Settings.CurrentTestGalaxy}.db";
            
            var testConnectionString = new SqliteConnectionStringBuilder 
                {DataSource = _fileName}.ConnectionString;

            var archive = new ArchiveConfig(Settings.CurrentTestGalaxy, ArchestraVersion.SystemPlatform2014);

            var builder = new ArchiveBuilder();
            builder.Build(archive, testConnectionString);
            
            _galaxyRepo = new GalaxyRepository(Settings.CurrentTestGalaxy);
            _galaxyRepo.Login(WindowsIdentity.GetCurrent().Name);
            _archiveRepo = new ArchiveRepository(testConnectionString);
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(_fileName))
                File.Delete(_fileName);
        }
        
        [Test]
        public void Construction_WhenCalled_ReturnsNotNull()
        {
            using var archiver = new GalaxyArchiver(_galaxyRepo, _archiveRepo);
            
            Assert.NotNull(archiver);
        }
        
        [Test]
        public void Archive_KnownGalaxyObject_ReturnsExpectedArchiveObject()
        {
            var knownObject = new ArchiveObject(14, "$UserDefined", 1, Template.UserDefined);
            using var archiver = new GalaxyArchiver(_galaxyRepo, _archiveRepo);
            
            archiver.Archive(knownObject);

            var result = _archiveRepo.GetObject(14);
            
            Assert.NotNull(result);
            Assert.AreEqual(knownObject.ObjectId, result.ObjectId);
            Assert.AreEqual(knownObject.TagName, result.TagName);
            Assert.AreEqual(knownObject.Version, result.Version);
            Assert.AreEqual(knownObject.Template, result.Template);
            Assert.IsNotEmpty(result.Entries);
            Assert.That(result.Entries, Has.Count.EqualTo(1));
        }
        
        [Test]
        public void ArchiveMultipleCalls_KnownGalaxyObject_ReturnsExpectedArchiveObjectSingleEntry()
        {
            var knownObject = new ArchiveObject(14, "$UserDefined", 1, Template.UserDefined);
            using var archiver = new GalaxyArchiver(_galaxyRepo, _archiveRepo);
            
            archiver.Archive(knownObject);
            archiver.Archive(knownObject);

            var result = _archiveRepo.GetObject(14);
            
            Assert.NotNull(result);
            Assert.AreEqual(knownObject.ObjectId, result.ObjectId);
            Assert.AreEqual(knownObject.TagName, result.TagName);
            Assert.AreEqual(knownObject.Version, result.Version);
            Assert.AreEqual(knownObject.Template, result.Template);
            Assert.IsNotEmpty(result.Entries);
            Assert.That(result.Entries, Has.Count.EqualTo(1));
        }
    }
}