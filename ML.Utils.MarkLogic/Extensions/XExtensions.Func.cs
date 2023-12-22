using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public static partial class XExtensions
    {
        #region ToFirst

        public static string ToFirst<TEntity>(this XJoin<TEntity> from, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return ToFirst<TEntity, TEntity>(from, builder);
        }

        public static string ToFirst<TEntity, TResult>(this XJoin<TEntity> from, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return from.Where(builder).SelectAll().ToFirst<TResult>();
        }

        #endregion

        #region ToList

        public static string ToList<TEntity>(this XJoin<TEntity> from, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return ToList<TEntity, TEntity>(from, filter, orderBy, select);
        }

        public static string ToList<TEntity, TResult>(this XJoin<TEntity> from, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return from.Where(filter).Order(orderBy).Select(select ?? XBuilder<TEntity>.Select.Fields()).ToList<TResult>();
        }

        #endregion

        #region Count

        public static string Count<TEntity>(this XJoin<TEntity> from, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return from.Where(builder).Count();
        }

        #endregion

        #region Max

        public static string Max<TEntity>(this XJoin<TEntity> from, Expression<Func<TEntity, object>> col, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return from.Where(builder).Max(col);
        }

        #endregion

        #region Min

        public static string Min<TEntity>(this XJoin<TEntity> from, Expression<Func<TEntity, object>> col, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return from.Where(builder).Min(col);
        }

        #endregion

        #region Sum

        public static string Sum<TEntity>(this XJoin<TEntity> from, Expression<Func<TEntity, object>> col, Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return from.Where(builder).Sum(col);
        }

        #endregion
    }
}
