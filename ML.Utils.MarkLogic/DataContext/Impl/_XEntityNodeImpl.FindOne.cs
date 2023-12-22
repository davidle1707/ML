using System;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override T FindOne(TId primaryValue)
        {
            return FindOne(b => b.Eq(_primaryNode, primaryValue));
        }

        public override Task<T> FindOneAsync(TId primaryValue)
        {
            return FindOneAsync(b => b.Eq(_primaryNode, primaryValue));
        }

        public override T FindOne(XFilter<T> filter = null)
        {
            return FindOne(b => filter);
        }

        public override Task<T> FindOneAsync(XFilter<T> filter = null)
        {
            return FindOneAsync(b => filter);
        }

        public override T FindOne(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            var query = From().ToFirst(builder);

            return FindOne(query);
        }

        public override Task<T> FindOneAsync(Func<XFilterBuilder<T>, XFilter<T>> builder)
        {
            var query = From().ToFirst(builder);

            return FindOneAsync(query);
        }

        public override T FindOne(string query)
        {
            var response = _db.ExecuteToObject<ExecuteResponse<T>>(query);

            return response.FirstOrDefault();
        }

        public override async Task<T> FindOneAsync(string query)
        {
            var response = await _db.ExecuteToObjectAsync<ExecuteResponse<T>>(query);

            return response.FirstOrDefault();
        }
    }
}
