using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.BusinessRule.Condition
{
    public class IfTag<T> : ThenTag<T> where T : class
    {
        public IfTag(string version = null)
            : base(version)
        { }

        public IfTag<T> If()
        {
            Rules.AppendLine(string.Format("<{0}>", Flow.If));
            Rules.AppendLine("<clause>");

            EndRules.Push(string.Format("</{0}>", Flow.If));
            EndRules.Push("</clause>");

            return this;
        }
    }
}
