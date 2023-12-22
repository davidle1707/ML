using System;

namespace ML.Utils.MarkLogic.Query
{
    [Serializable]
    internal class _FromInfo
    {
        public string Alias { get; set; }

        public string DocumentQuery { get; set; }

        public string Condition { get; set; }

        public XEntityAttribute Attribute { get; set; }

        public bool IsValid => !string.IsNullOrWhiteSpace(DocumentQuery);

        public string ToQuery()
        {
            var query = $"for {Alias} in {DocumentQuery} ";

            if (!string.IsNullOrWhiteSpace(Condition))
            {
                query = $"{query.TrimEnd()}[{Condition}]";
            }

            return query;
        }
    }
}
