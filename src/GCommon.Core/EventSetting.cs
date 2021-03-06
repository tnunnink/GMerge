using GCommon.Core.Enumerations;

namespace GCommon.Core
{
    public class EventSetting
    {
        private EventSetting()
        {
        }
        
        public EventSetting(Operation operation, bool isArchiveEvent = false)
        {
            Operation = operation;
            OperationType = OperationType.FromOperation(operation);
            IsArchiveEvent = isArchiveEvent;
        }

        public int EventId { get; private set; }
        public int ArchiveId { get; private set; }
        public ArchiveConfig ArchiveConfig { get; private set; }
        public Operation Operation { get; private set; }
        public OperationType OperationType { get; private set; }
        public bool IsArchiveEvent { get; set; }
    }
}