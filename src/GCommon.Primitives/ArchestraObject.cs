using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using GCommon.Differencing;
using GCommon.Differencing.Abstractions;
using GCommon.Primitives.Base;
using GCommon.Primitives.Enumerations;

namespace GCommon.Primitives
{
    public class ArchestraObject : IXSerializable, IDifferentiable<ArchestraObject>
    {
        public ArchestraObject()
        {
        }

        private ArchestraObject(XElement element)
        {
            TagName = element.Attribute(nameof(TagName))?.Value;
            HierarchicalName = element.Attribute(nameof(HierarchicalName))?.Value;
            ContainedName = element.Attribute(nameof(ContainedName))?.Value;
            ConfigVersion = Convert.ToInt32(element.Attribute(nameof(ConfigVersion))?.Value);
            Category = ObjectCategory.FromName(element.Attribute(nameof(Category))?.Value);
            DerivedFromName = element.Attribute(nameof(DerivedFromName))?.Value;
            BasedOnName = element.Attribute(nameof(BasedOnName))?.Value;
            HostName = element.Attribute(nameof(HostName))?.Value;
            AreaName = element.Attribute(nameof(AreaName))?.Value;
            ContainerName = element.Attribute(nameof(ContainerName))?.Value;
            Attributes = element.Descendants("Attribute").Select(ArchestraAttribute.Materialize).ToList();
        }

        public string TagName { get; set; }
        public string ContainedName { get; set; }
        public string HierarchicalName { get; set; }
        public ObjectCategory Category { get; set; }
        public int ConfigVersion { get; set; }
        public string DerivedFromName { get; set; }
        public string BasedOnName { get; set; }
        public string HostName { get; set; }
        public string AreaName { get; set; }
        public string ContainerName { get; set; }
        public IEnumerable<ArchestraAttribute> Attributes { get; set; }

        public static ArchestraObject Materialize(XElement element)
        {
            return new ArchestraObject(element);
        }

        public XElement Serialize()
        {
            var root = new XElement("Object");
            root.Add(new XAttribute(nameof(TagName), TagName ?? string.Empty));
            root.Add(new XAttribute(nameof(HierarchicalName), HierarchicalName ?? string.Empty));
            root.Add(new XAttribute(nameof(ContainedName), ContainedName ?? string.Empty));
            root.Add(new XAttribute(nameof(ConfigVersion), ConfigVersion));
            root.Add(new XAttribute(nameof(Category), Category ?? ObjectCategory.Undefined));
            root.Add(new XAttribute(nameof(DerivedFromName), DerivedFromName ?? string.Empty));
            root.Add(new XAttribute(nameof(BasedOnName), BasedOnName ?? string.Empty));
            root.Add(new XAttribute(nameof(HostName), HostName ?? string.Empty));
            root.Add(new XAttribute(nameof(AreaName), AreaName ?? string.Empty));
            root.Add(new XAttribute(nameof(ContainerName), ContainerName ?? string.Empty));

            if (Attributes != null)
                root.Add(new XElement("Attributes", Attributes.Select(a => a.Serialize())));

            return root;
        }

        public bool Equals(ArchestraObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TagName == other.TagName && ContainedName == other.ContainedName &&
                   HierarchicalName == other.HierarchicalName && Equals(Category, other.Category) &&
                   ConfigVersion == other.ConfigVersion && DerivedFromName == other.DerivedFromName &&
                   BasedOnName == other.BasedOnName && HostName == other.HostName && AreaName == other.AreaName &&
                   ContainerName == other.ContainerName && Equals(Attributes, other.Attributes);
        }
        
        public IEnumerable<Difference> DiffersFrom(ArchestraObject other)
        {
            var differences = new List<Difference>();

            differences.AddRange(Difference.Between(this, other, x => x.TagName));
            differences.AddRange(Difference.Between(this, other, x => x.ContainedName));
            differences.AddRange(Difference.Between(this, other, x => x.HierarchicalName));
            differences.AddRange(Difference.Between(this, other, x => x.Category));
            differences.AddRange(Difference.Between(this, other, x => x.ConfigVersion));
            differences.AddRange(Difference.Between(this, other, x => x.DerivedFromName));
            differences.AddRange(Difference.Between(this, other, x => x.BasedOnName));
            differences.AddRange(Difference.Between(this, other, x => x.HostName));
            differences.AddRange(Difference.Between(this, other, x => x.AreaName));
            differences.AddRange(Difference.Between(this, other, x => x.ContainerName));
            differences.AddRange(Difference.BetweenCollection(Attributes, other.Attributes, a => a.Name));

            return differences;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ArchestraObject) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (TagName != null ? TagName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContainedName != null ? ContainedName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HierarchicalName != null ? HierarchicalName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Category != null ? Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ConfigVersion;
                hashCode = (hashCode * 397) ^ (DerivedFromName != null ? DerivedFromName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BasedOnName != null ? BasedOnName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HostName != null ? HostName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AreaName != null ? AreaName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ContainerName != null ? ContainerName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Attributes != null ? Attributes.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(ArchestraObject left, ArchestraObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ArchestraObject left, ArchestraObject right)
        {
            return !Equals(left, right);
        }
    }
}