using ArchestrA.GRAccess;
using GCommon.Core.Enumerations;
using GCommon.Core.Structs;
using GServer.Archestra.Helpers;

namespace GServer.Archestra.Extensions
{
    internal static class TypeExtensions
    {
        public static ValidationStatus ToPrimitive(this EPACKAGESTATUS ePackageStatus)
        {
            return ValidationStatus.FromValue((int) ePackageStatus);
        }

        public static EPACKAGESTATUS ToMx(this ValidationStatus validationStatus)
        {
            return (EPACKAGESTATUS) validationStatus.Value;
        }
        
        public static ObjectCategory ToPrimitive(this ECATEGORY category)
        {
            return ObjectCategory.FromValue((int) category);
        }

        public static ECATEGORY ToMx(this ObjectCategory category)
        {
            return (ECATEGORY) category.Value;
        }

        public static DataType ToPrimitive(this MxDataType mxDataType)
        {
            return DataType.FromValue((int) mxDataType);
        }

        public static MxDataType ToMx(this DataType dataType)
        {
            return (MxDataType) dataType.Value;
        }

        public static AttributeCategory ToPrimitive(this MxAttributeCategory mxAttributeCategory)
        {
            return AttributeCategory.FromValue((int) mxAttributeCategory);
        }

        public static MxAttributeCategory ToMx(this AttributeCategory attributeCategory)
        {
            return (MxAttributeCategory) attributeCategory.Value;
        }

        public static SecurityClassification ToPrimitive(this MxSecurityClassification mxSecurityClassification)
        {
            return SecurityClassification.FromValue((int) mxSecurityClassification);
        }

        public static MxSecurityClassification ToMx(this SecurityClassification securityClassification)
        {
            return (MxSecurityClassification) securityClassification.Value;
        }

        public static LockType ToPrimitive(this MxPropertyLockedEnum mxPropertyLocked)
        {
            return LockType.FromValue((int) mxPropertyLocked);
        }

        public static MxPropertyLockedEnum ToMx(this LockType lockType)
        {
            return (MxPropertyLockedEnum) lockType.Value;
        }

        public static Reference ToPrimitive(this IMxReference mxReference)
        {
            return new Reference(mxReference.FullReferenceString);
        }

        public static IMxReference ToMx(this Reference reference)
        {
            return MxReference.Create(reference);
        }

        public static StatusCategory ToPrimitive(this MxStatusCategory mxStatusCategory)
        {
            return StatusCategory.FromValue((int) mxStatusCategory);
        }

        public static MxStatusCategory ToMx(this StatusCategory statusCategory)
        {
            return (MxStatusCategory) statusCategory.Value;
        }

        public static Quality ToPrimitive(this DataQuality mxDataQuality)
        {
            return Quality.FromValue((int) mxDataQuality);
        }

        public static DataQuality ToMx(this Quality quality)
        {
            return (DataQuality) quality.Value;
        }
    }
}