using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override bool Insert(T source)
        {
            var query = _nodeFunc.Insert(source, _primaryNode);

            return _db.Execute(query);
        }

        public override Task<bool> InsertAsync(T source)
        {
            var query = _nodeFunc.Insert(source, _primaryNode);

            return _db.ExecuteAsync(query);
        }

        public override bool InsertMany(List<T> sources)
        {
            var query = _nodeFunc.InsertMany(sources, _primaryNode);

            return _db.Execute(query);
        }

        public override Task<bool> InsertManyAsync(List<T> sources)
        {
            var query = _nodeFunc.InsertMany(sources, _primaryNode);

            return _db.ExecuteAsync(query);
        }

        public override bool InsertField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            var query = _nodeFunc.InsertField(primaryValue, fieldNode, fieldValue);

            return _db.Execute(query);
        }

        public override Task<bool> InsertFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            var query = _nodeFunc.InsertField(primaryValue, fieldNode, fieldValue);

            return _db.ExecuteAsync(query);
        }

        public override bool InsertElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, TElem elem)
        {
            var query = _nodeFunc.InsertElem(primaryValue, elemsNode, elem);

            return _db.Execute(query);
        }

        public override Task<bool> InsertElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, TElem elem)
        {
            var query = _nodeFunc.InsertElem(primaryValue, elemsNode, elem);

            return _db.ExecuteAsync(query);
        }
    }
}
