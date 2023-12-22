using System;

namespace ML.Utils.MarkLogic.Query
{
    [Serializable]
    internal class _OrderByInfo
    {
        public _OrderByInfo()
        {
        }

        public _OrderByInfo(string nodeName) : this()
        {
            NodeName = nodeName;
        }

        public string NodeName { get; set; }

        public bool TypeIsString { get; set; }

        public bool TypeIsNullable { get; set; }

        public bool IsDescending { get; set; }

        public string ToQuery()
        {
            if (TypeIsString)
            {
                return $"fn:lower-case({NodeName}) {(IsDescending ? "descending" : "ascending")}";
            }

            return TypeIsNullable
               ? $"(if(not({NodeName}/node())) then () else {NodeName}) {(IsDescending ? "descending" : "ascending")}"
               : $"{NodeName} {(IsDescending ? "descending" : "ascending")}";
        }
    }
}
