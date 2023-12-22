using System;
namespace ML.BusinessRule
{
    public struct OperatorType
    {
        public const string String = "string";
        public const string Numeric = "numeric";
        public const string DateTime = "System.DateTime";
        public const string Bool = "bool";
        public const string Enum = "enum";

        public static string GetType(object value)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(DateTime))
                {
                    return DateTime;
                }
                else if (value.GetType() == typeof(string)
                    || value.GetType() == typeof(Guid))
                {
                    return String;
                }
                else if (value.GetType() == typeof(bool))
                {
                    return Bool;
                }
                else if (value.GetType() == typeof(Enum))
                {
                    return Enum;
                }
                else
                {
                    return Numeric;
                }
            }
            return null;
        }
    }
}
