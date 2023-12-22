using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ML.Common;

namespace ML.BusinessRule.Condition
{
    public class ConditionTag<T> where T : class
    {
        protected StringBuilder Rules { get; set; }

        /// <summary>
        /// Condition for property
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public ConditionTag(Expression<Func<T, object>> prop, string @operator, object value, bool stringComparison = false)
        {
            Rules = new StringBuilder();

            if (stringComparison)
            {
                Rules.AppendLine(string.Format("<condition type=\"{0}\" stringComparison=\"OrdinalIgnoreCase\">", @operator));
            }
            else
            {
                Rules.AppendLine(string.Format("<condition type=\"{0}\">", @operator));
            }

            Rules.AppendLine(string.Format("<property name=\"{0}\" />", std.PropName2(prop)));

            var dataType = OperatorType.GetType(value);
            if (!string.IsNullOrWhiteSpace(dataType))
            {
                Rules.AppendLine(string.Format("<value type=\"{0}\">{1}</value>", dataType, value));
            }
            else
            {
                Rules.AppendLine(string.Format("<value>{0}</value>", value));
            }

            Rules.Append("</condition>");
        }

        /// <summary>
        /// Condition for method
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        //public ConditionTag(MethodTag method, Operators @operator, Expression<Func<T, object>> prop, bool stringComparison = false)
        //{
        //    if (method == null)
        //    {
        //        throw new ArgumentNullException("method");
        //    }

        //    Rules = new StringBuilder();

        //    if (stringComparison)
        //    {
        //        Rules.AppendLine(string.Format("<condition type=\"{0}\" stringComparison=\"OrdinalIgnoreCase\">", @operator));
        //    }
        //    else
        //    {
        //        Rules.AppendLine(string.Format("<condition type=\"{0}\">", @operator));
        //    }

        //    Rules.AppendLine(method.GetXml());

        //    Rules.AppendLine(string.Format("<property name=\"{0}\" />", std.PropName2(prop)));

        //    Rules.AppendLine("</condition>");
        //}

        public virtual string GetXml()
        {
            if (Rules != null)
            {
                return Rules.ToString();
            }
            return string.Empty;
        }
    }
}
