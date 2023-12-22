using System;
using System.Threading.Tasks;
using ML.Common;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override int Count(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            return Count(XExtensions.GetFilter(builder));
        }

        public override Task<int> CountAsync(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            return CountAsync(XExtensions.GetFilter(builder));
        }

        public override int Count(XFilter<T> filter)
        {
            var query = From().Where(filter).Count();

            return _db.ExecuteScalar(query).ToInt();
        }

        public override async Task<int> CountAsync(XFilter<T> filter)
        {
            var query = From().Where(filter).Count();
            var scalar = await _db.ExecuteScalarAsync(query);

            return scalar.ToInt();
        }
    }
}
