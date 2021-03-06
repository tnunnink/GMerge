using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using GCommon.Core.Abstractions;
using GCommon.Core.Enumerations;

// Object will be deserialized via xml
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GCommon.Core
{
    public class NamedScript : IXSerializable
    {
        public string Name { get; set; }
        public int DeadBand { get; set; }
        public string Expression { get; set; }
        public ScriptTrigger Trigger { get; set; }
        public int TriggerPeriod { get; set; }
        public string Text { get; set; }

        public NamedScript Materialize(XElement element)
        {
            Name = element.Attribute(nameof(Name))?.Value;
            
            var deadBand = element.Attribute(nameof(DeadBand))?.Value;
            if (deadBand != null)
                DeadBand = int.Parse(deadBand);
            
            Expression = element.Element("ScriptExpression")?.Attribute(nameof(Expression))?.Value;
            
            var script = element.Element("Script")?.Descendants().FirstOrDefault(s => s.NodeType == XmlNodeType.Element);
            if (script == null) return this;
            
            Trigger = (ScriptTrigger) Enum.Parse(typeof(ScriptTrigger), script.Name.ToString());

            var period = script.Attribute("TriggerPeriodMS")?.Value;
            if (period != null)
                TriggerPeriod = int.Parse(period);
            
            Text = script.Value;

            return this;
        }

        public XElement Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}