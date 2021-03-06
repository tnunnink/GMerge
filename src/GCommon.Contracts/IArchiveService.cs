using System.Collections.Generic;
using System.ServiceModel;

namespace GCommon.Contracts
{
    [ServiceContract(Namespace = "http://www.gmerge.com/Contracts")]
    public interface IArchiveService
    {
        [OperationContract]
        bool Connect(string galaxyName);
        
        [OperationContract]
        ArchiveObjectData GetArchiveObject(int objectId);

        [OperationContract]
        IEnumerable<ArchiveObjectData> GetArchiveObjects();

        [OperationContract]
        IEnumerable<ArchiveEntryData> GetArchiveEntries();

        [OperationContract]
        GalaxyObjectData GetGalaxyObject(int objectId);
        
        [OperationContract]
        GalaxySymbolData GetGalaxySymbol(int objectId);

        [OperationContract]
        IEnumerable<EventSettingData> GetEventSettings();
        
        [OperationContract]
        IEnumerable<InclusionSettingData> GetInclusionSettings();
        
        [OperationContract]
        void ArchiveObject(int objectId);
        
        [OperationContract]
        void RemoveObject(int objectId);

        [OperationContract]
        void UpdateEventSetting(IEnumerable<EventSettingData> eventSettings);

        [OperationContract]
        void UpdateInclusionSetting(IEnumerable<InclusionSettingData> inclusionSettings);
    }
}