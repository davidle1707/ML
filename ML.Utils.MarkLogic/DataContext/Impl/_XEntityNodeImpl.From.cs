using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override XJoin<T> From(string alias = null)
        {
            return XBuilder<T>.From(_db.DbPath, alias);
        }

        public override XJoin<TElem> FromElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null)
        {
            return XBuilder<T>.FromElem(_db.DbPath, _primaryNode, primaryValue, elemsNode, alias);
        }

        public override XJoin<TElem> FromElem<TElem>(IEnumerable<TId> primaryValues, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null)
        {
            return XBuilder<T>.FromElem(_db.DbPath, _primaryNode, primaryValues, elemsNode, alias);
        }
    }
}
