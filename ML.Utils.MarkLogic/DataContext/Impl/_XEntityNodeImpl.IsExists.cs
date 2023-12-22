using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ML.Common;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override bool IsExists(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            return IsExists(XExtensions.GetFilter(builder));
        }

        public override Task<bool> IsExistsAsync(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            return IsExistsAsync(XExtensions.GetFilter(builder));
        }

        public override bool IsExists(XFilter<T> filter)
        {
            return Count(filter) > 0;
        }

        public override async Task<bool> IsExistsAsync(XFilter<T> filter)
        {
            var count = await CountAsync(filter);

            return count > 0;
        }

        public override bool IsExists<TField, TIgnore>(Expression<Func<T, TField>> col, TField value, Expression<Func<T, TIgnore>> ignoreCol, TIgnore ignoreValue)
        {
            var bf = XBuilder<T>.Filter;
            var query = From().Where(bf.Eq(col, value) & bf.NotEq(ignoreCol, ignoreValue)).Count();

            var count = _db.ExecuteScalar(query);

            return count.ToInt() > 0;
        }

        public override async Task<bool> IsExistsAsync<TField, TIgnore>(Expression<Func<T, TField>> col, TField value, Expression<Func<T, TIgnore>> ignoreCol, TIgnore ignoreValue)
        {
            var bf = XBuilder<T>.Filter;
            var query = From().Where(bf.Eq(col, value) & bf.NotEq(ignoreCol, ignoreValue)).Count();

            var count = await _db.ExecuteScalarAsync(query);

            return count.ToInt() > 0;
        }

        public override bool IsFieldExists<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode)
        {
            var query = _nodeFunc.IsFieldExists(primaryValue, fieldNode);
            var value = _db.ExecuteToString(query);

            return value.ToInt() > 0;
        }

        public override async Task<bool> IsFieldExistsAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode)
        {
            var query = _nodeFunc.IsFieldExists(primaryValue, fieldNode);
            var value = await _db.ExecuteToStringAsync(query);

            return value.ToInt() > 0;
        }

        public override bool IsExists<TField>(Expression<Func<T, TField>> col, TField value)
        {
            var query = _nodeFunc.IsExists(col, value);
            var valueAsString = _db.ExecuteToString(query);

            return valueAsString.ToInt() > 0;
        }

        public override async Task<bool> IsExistsAsync<TField>(Expression<Func<T, TField>> col, TField value)
        {
            var query = _nodeFunc.IsExists(col, value);
            var valueAsString = await _db.ExecuteToStringAsync(query);

            return valueAsString.ToInt() > 0;
        }
    }
}
