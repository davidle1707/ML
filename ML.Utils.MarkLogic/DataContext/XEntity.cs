using System;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XEntity<TId>
    {
        public virtual TId Id { get; set; }
    }
}
