using System.Xml.Linq;
using GCommon.Core.Abstractions;

namespace GCommon.Core
{
    public class PredefinedScript : IXSerializable
    {
        public string Name { get; set; }
        public string Text { get; set; }
        
        public PredefinedScript Materialize(XElement element)
        {
            Name = element.Name.ToString();
            Text = element.Value;
            return this;
        }

        public XElement Serialize()
        {
            return new XElement(Name, Text);
        }
    }
}