using ML.Common;
using ML.Utils.MarkLogic.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ML.Utils.MarkLogic
{
    public class XOrderByBuilder
    {
        internal readonly List<_OrderByInfo> Infos = new List<_OrderByInfo>();

        internal XOrderByBuilder()
        {
        }
    }

    public class XOrderByBuilder<T> : XOrderByBuilder
    {
        internal XOrderByBuilder()
        {
        }

        public XOrderByBuilder<T> Descending(Expression<Func<T, object>> col, string alias = null)
        {
            return Process(col, alias, true);
        }

        public XOrderByBuilder<T> Ascending(Expression<Func<T, object>> col, string alias = null)
        {
            return Process(col, alias, false);
        }

        private XOrderByBuilder<T> Process(Expression<Func<T, object>> col, string alias, bool descending)
        {
            var propType = ((PropertyInfo)std.MemberInfo(col)).PropertyType;

            Infos.Add(new _OrderByInfo
            {
                NodeName = col.NodeNameWithAlias(alias),
                IsDescending = descending,
                TypeIsString = propType == typeof(string),
                TypeIsNullable = propType.IsNullableType(),
            });

            return this;
        }
    }
}
