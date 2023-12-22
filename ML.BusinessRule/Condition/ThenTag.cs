using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.BusinessRule.Condition
{
    public class ThenTag<T> : OutputTag<T> where T : class
    {
        public ThenTag(string version = null)
            : base(version)
        { }

        public ThenTag<T> Then(MethodTag method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            Rules.AppendLine(EndRules.Pop());
            Rules.AppendLine("<then>");
            Rules.AppendLine(method.GetXml());
            Rules.AppendLine("</then>");

            return this;
        }

        public ThenTag<T> And(bool isBlock = false, params ConditionTag<T>[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException("conditions");
            }

            if (isBlock)
            {
                Rules.AppendLine("<and ui:block=\"true\">");
            }
            else
            {
                Rules.AppendLine("<and>");
            }

            foreach (var item in conditions)
                Rules.AppendLine(item.GetXml());

            Rules.AppendLine("</and>");

            return this;
        }

        public ThenTag<T> Or(bool isBlock = false, params ConditionTag<T>[] conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException("conditions");
            }

            if (isBlock)
            {
                Rules.AppendLine("<or ui:block=\"true\">");
            }
            else
            {
                Rules.AppendLine("<or>");
            }

            foreach (var item in conditions)
            {
                Rules.AppendLine(item.GetXml());
            }

            Rules.AppendLine("</or>");

            return this;
        }

        public ThenTag<T> ElseIf()
        {
            Rules.AppendLine(string.Format("<{0}>", Flow.Else));
            Rules.AppendLine(string.Format("<{0}>", Flow.If));
            Rules.AppendLine("<clause>");

            EndRules.Push(string.Format("</{0}>", Flow.Else));
            EndRules.Push(string.Format("</{0}>", Flow.If));
            EndRules.Push("</clause>");

            return this;
        }

        public ThenTag<T> Else(MethodTag method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            Rules.AppendLine(string.Format("<{0}>", Flow.Else));
            Rules.AppendLine(method.GetXml());
            Rules.AppendLine(string.Format("</{0}>", Flow.Else));

            return this;
        }
    }
}
