using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override bool Delete(TId primaryValue)
        {
            var query = _nodeFunc.Delete(primaryValue);

            return _db.Execute(query);
        }

        public override Task<bool> DeleteAsync(TId primaryValue)
        {
            var query = _nodeFunc.Delete(primaryValue);

            return _db.ExecuteAsync(query);
        }

        public override bool DeleteElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            var query = _nodeFunc.DeleteElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue);

            return _db.Execute(query);
        }

        public override Task<bool> DeleteElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            var query = _nodeFunc.DeleteElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue);

            return _db.ExecuteAsync(query);
        }
    }
}
