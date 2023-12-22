using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override bool Save(bool isNew, T source)
        {
            return isNew ? Insert(source) : Update(source);
        }

        public override Task<bool> SaveAsync(bool isNew, T source)
        {
            return isNew ? InsertAsync(source) : UpdateAsync(source);
        }

        public override bool Save(T source)
        {
            var primaryValue = _primaryNode.Compile()(source);

            return IsExists(_primaryNode, primaryValue) ? Update(source) : Insert(source);
        }

        public override async Task<bool> SaveAsync(T source)
        {
            var primaryValue = _primaryNode.Compile()(source);

            var exists = await IsExistsAsync(_primaryNode, primaryValue);
            var success = exists ? await UpdateAsync(source) : await InsertAsync(source);

            return success;
        }

        public override bool SaveField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            return IsFieldExists(primaryValue, fieldNode) ? UpdateField(primaryValue, fieldNode, fieldValue) : InsertField(primaryValue, fieldNode, fieldValue);
        }

        public override async Task<bool> SaveFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue)
        {
            var exists = await IsFieldExistsAsync(primaryValue, fieldNode);
            var success = exists ? await UpdateFieldAsync(primaryValue, fieldNode, fieldValue) : await InsertFieldAsync(primaryValue, fieldNode, fieldValue);

            return success;
        }
    }
}
