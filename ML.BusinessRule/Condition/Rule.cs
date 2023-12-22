using System;
using System.Collections.Generic;
using System.Text;

namespace ML.BusinessRule.Condition
{
    [Serializable]
    public class Rule
    {
        public Guid Id { get; set; }
        public string WebRule { get; set; }
        public DateTime UtcTime { get; set; }
        public TypeInfo TypeInfo { get; set; }
        internal StringBuilder Rules { get; set; }
        internal Stack<string> EndRules { get; set; }

        public Rule(Type type, string version = null)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            Id = Guid.NewGuid();
            WebRule = version ?? "4.1.6.4";
            UtcTime = DateTime.UtcNow;
            TypeInfo = new TypeInfo(type);
            Rules = new StringBuilder();

            EndRules = new Stack<string>();

            Rules.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Rules.AppendLine("<codeeffects xmlns=\"http://codeeffects.com/schemas/rule/41\" xmlns:ui=\"http://codeeffects.com/schemas/ui/4\">");
            Rules.AppendLine(ToString());
            Rules.AppendLine("<definition>");

            EndRules.Push("</codeeffects>");
            EndRules.Push("</rule>");
            EndRules.Push("</definition>");
        }

        public override string ToString()
        {
            return String.Format("<rule id=\"{0}\" webrule=\"{1}\" utc=\"{2:s}\" type=\"{3}\" eval=\"false\">", Id, WebRule, UtcTime, TypeInfo);
        }
    }
}
