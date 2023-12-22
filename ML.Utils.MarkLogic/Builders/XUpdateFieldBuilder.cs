using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class XUpdateFieldBuilder<T>
    {
        public static XUpdateFieldBuilder<T> operator &(XUpdateFieldBuilder<T> left, XUpdateFieldBuilder<T> right)
        {
            return left.Join(right);
        }

        internal readonly List<Expression<Func<T, object>>> _fields = new List<Expression<Func<T, object>>>();
        internal readonly List<object> _values = new List<object>();

        internal XUpdateFieldBuilder()
        {
        }

        internal XUpdateFieldBuilder<T> Join(XUpdateFieldBuilder<T> extend)
        {
            _fields.AddRange(extend._fields);
            _values.AddRange(extend._values);

            return this;
        }

        public XUpdateFieldBuilder<T> Set(Expression<Func<T, object>> field, object value)
        {
            _fields.Add(field);
            _values.Add(value);

            return this;
        }

       internal XUpdateField<T>[] Fields => _fields.Select((t, i) => new XUpdateField<T>(t, _values[i])).ToArray();

    }
}
