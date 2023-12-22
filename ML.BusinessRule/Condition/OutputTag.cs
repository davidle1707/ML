using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.BusinessRule.Condition
{
    public class OutputTag<T> : Rule where T : class
    {
        public OutputTag(string version = null)
            : base(typeof(T), version)
        { }

        public OutputTag<T> Output()
        {
            //end if else clause
            while(EndRules.Count > 2)
            {
                if (EndRules.Peek() == "</definition>")
                {
                    Rules.AppendLine(EndRules.Pop());
                    break;
                }
                Rules.AppendLine(EndRules.Pop());
            }

            Rules.AppendLine("<format><lines /></format>");

            //end xml document
            while (EndRules.Count > 0)
            {
                Rules.AppendLine(EndRules.Pop());
            }

            return this;
        }
    }
}
