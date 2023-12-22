using System;

namespace ML.Utils.MarkLogic
{
    public static partial class XExtensions
    {
        internal static XFilter<TEntity> GetFilter<TEntity>(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder) => builder(XBuilder<TEntity>.Filter);
    }
}
