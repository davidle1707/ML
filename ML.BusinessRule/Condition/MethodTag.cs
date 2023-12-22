using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.BusinessRule.Condition
{
    public class MethodTag
    {
        protected StringBuilder Rules { get; set; }

        public MethodTag(string methodName, TypeInfo typeInfo = null, params object[] values)
        {
            if (methodName == null)
            {
                throw new ArgumentNullException("methodName");
            }

            Rules = new StringBuilder();

            if (typeInfo != null)
            {
                Rules.AppendLine(string.Format("<method name=\"{0}\" type=\"{1}\">", methodName, typeInfo));
            }
            else
            {
                Rules.AppendLine(string.Format("<method name=\"{0}\">", methodName));
            }

            foreach(var item in values)
            {
                var dataType = OperatorType.GetType(item);
                if (dataType != null)
                {
                    Rules.AppendLine(string.Format("<value type=\"{0}\">{1}</value>", dataType, item));
                }
                else
                {
                    Rules.AppendLine(string.Format("<value>{0}</value>", item));
                }
            }

            Rules.Append("</method>");
        }

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
