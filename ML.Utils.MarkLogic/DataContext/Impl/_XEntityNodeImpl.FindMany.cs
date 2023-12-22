using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId>
    {
        public override List<T> FindMany(XFilter<T> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return FindMany(b => filter, orderBy, select);
        }

        public override Task<List<T>> FindManyAsync(XFilter<T> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return FindManyAsync(b => filter, orderBy, select);
        }

        public override List<T> FindMany(Func<XFilterBuilder<T>, XFilter<T>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            var query = From().ToList(filter, orderBy, select);

            return FindMany(query).Items;
        }

        public override async Task<List<T>> FindManyAsync(Func<XFilterBuilder<T>, XFilter<T>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            var query = From().ToList(filter, orderBy, select);
            var response = await FindManyAsync(query);

            return response.Items;
        }

        public override ExecuteResponse<T> FindMany(string query)
        {
            return _db.ExecuteToObject<ExecuteResponse<T>>(query);
        }

        public override Task<ExecuteResponse<T>> FindManyAsync(string query)
        {
            return _db.ExecuteToObjectAsync<ExecuteResponse<T>>(query);
        }
    }
}
