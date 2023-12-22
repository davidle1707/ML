using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class XUpdateField<T>
    {
        public Expression<Func<T, object>> Node { get; private set; }

        public object Value { get; private set; }

        internal XUpdateField(Expression<Func<T, object>> node, object value)
        {
            Node = node;
            Value = value;
        }
    }
}
