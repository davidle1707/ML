using System;
using System.Collections.Generic;

namespace ML.BusinessRule.Condition
{
    [Serializable]
    public class TypeInfo
    {
        public Type Type { get; set; }

        public TypeInfo(Type type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Type.FullName, Type.Assembly.ToString());
        }
    }
}
