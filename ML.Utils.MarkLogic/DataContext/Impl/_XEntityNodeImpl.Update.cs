using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override bool Update(T source)
        {
            var query = _nodeFunc.Update(source, _primaryNode);

            return _db.Execute(query);
        }

        public override Task<bool> UpdateAsync(T source)
        {
            var query = _nodeFunc.Update(source, _primaryNode);

            return _db.ExecuteAsync(query);
        }

        public override bool UpdateMany(List<T> sources)
        {
            var query = _nodeFunc.UpdateMany(sources, _primaryNode);

            return _db.Execute(query);
        }

        public override Task<bool> UpdateManyAsync(List<T> sources)
        {
            var query = _nodeFunc.UpdateMany(sources, _primaryNode);

            return _db.ExecuteAsync(query);
        }

        public override bool UpdateField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            var query = _nodeFunc.UpdateField(primaryValue, fieldNode, fieldValue);

            return _db.Execute(query);
        }

        public override Task<bool> UpdateFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            var query = _nodeFunc.UpdateField(primaryValue, fieldNode, fieldValue);

            return _db.ExecuteAsync(query);
        }

        public override bool UpdateField(TId primaryValue, XUpdateFieldBuilder<T> builder)
        {
            var fields = builder.Fields;

            if (fields.Length == 0)
            {
                return false;
            }

            var query = _nodeFunc.UpdateFields(primaryValue, fields);

            return _db.Execute(query);
        }

        public override async Task<bool> UpdateFieldAsync(TId primaryValue, XUpdateFieldBuilder<T> builder)
        {
            var fields = builder.Fields;

            if (fields.Length == 0)
            {
                return false;
            }

            var query = _nodeFunc.UpdateFields(primaryValue, fields);
            var success = await _db.ExecuteAsync(query);

            return success;
        }

        public override bool UpdateElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            var query = _nodeFunc.UpdateElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue, elem);

            return _db.Execute(query);
        }

        public override Task<bool> UpdateElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            var query = _nodeFunc.UpdateElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue, elem);

            return _db.ExecuteAsync(query);
        }
    }
}
