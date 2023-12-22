using System.Collections.Generic;

namespace ML.Utils.MarkLogic
{
    public static class XFitlerExtensions
    {
        public static XFilter<TEntity> And<TEntity>(this XFilter<TEntity> filter, params XFilter<TEntity>[] others)
        {
            if (others.Length == 0)
            {
                return filter;
            }

            var filters = new List<XFilter<TEntity>> { filter };
            filters.AddRange(others);

            return new XAndOrFilter<TEntity>(filters, "and");
        }

        public static XFilter<TEntity> Or<TEntity>(this XFilter<TEntity> filter, params XFilter<TEntity>[] others)
        {
            if (others.Length == 0)
            {
                return filter;
            }

            var filters = new List<XFilter<TEntity>> { filter };
            filters.AddRange(others);

            return new XAndOrFilter<TEntity>(filters, "or");
        }

        public static XFilter<TEntity> And<TEntity, TEntity2>(this XFilter<TEntity> filter, XFilter<TEntity2> filter2)
        {
            return new XAndOrFilter<TEntity, TEntity2>(filter, filter2, "and");
        }

        public static XFilter<TEntity> Or<TEntity, TEntity2>(this XFilter<TEntity> filter, XFilter<TEntity2> filter2)
        {
            return new XAndOrFilter<TEntity, TEntity2>(filter, filter2, "or");
        }
    }
}
