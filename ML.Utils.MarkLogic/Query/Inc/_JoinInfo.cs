using System;

namespace ML.Utils.MarkLogic.Query
{
    [Serializable]
    internal class _JoinInfo
    {
        public _JoinInfo(bool inner = true)
        {
            IsInner = inner;
        }

        public bool IsInner { get; }

        public string Alias { get; set; }

        public string DocumentQuery { get; set; }

        public XEntityAttribute Attribute { get; set; }

        public string Condition { get; set; }

        public string ToQuery()
        {
            return IsInner 
                ? $"for {Alias} in {DocumentQuery}[{Condition}] "
                : $"let {Alias} := {DocumentQuery}[{Condition}] ";
        }
    }
}
