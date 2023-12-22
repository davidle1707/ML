using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class XChildMap<TFrom, TTo>
    {
        public XChildMap(Expression<Func<TFrom, object>> from, Expression<Func<TTo, object>> to)
        {
            From = from;
            To = to;
        }

        public Expression<Func<TFrom, object>> From { get; private set; }

        public Expression<Func<TTo, object>> To { get; private set; }
    }
}
