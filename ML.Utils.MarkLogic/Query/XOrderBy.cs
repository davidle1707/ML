using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ML.Common;
using ML.Utils.MarkLogic.Query;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XOrderBy<TEntity> : XSelect<TEntity>
    {
        internal XOrderBy(string dbPath)
            : base(dbPath)
        {

        }

        public XOrderBy<TEntity> OrderEmpty()
        {
            return this;
        }

        #region TEntity

        public XOrderBy<TEntity> OrderBy(Expression<Func<TEntity, object>> col, string alias = null)
        {
            return Order(XBuilder<TEntity>.OrderBy.Ascending(col, alias));
        }

        public XOrderBy<TEntity> OrderByDescending(Expression<Func<TEntity, object>> col, string alias = null)
        {
            return Order(XBuilder<TEntity>.OrderBy.Descending(col, alias));
        }

        #endregion

        #region Generic

        public XOrderBy<TEntity> OrderBy<TOrder>(Expression<Func<TOrder, object>> col, string alias = null)
        {
            return Order(XBuilder<TOrder>.OrderBy.Ascending(col, alias));
        }

        public XOrderBy<TEntity> OrderByDescending<TOrder>(Expression<Func<TOrder, object>> col, string alias = null)
        {
            return Order(XBuilder<TOrder>.OrderBy.Descending(col, alias));
        }
        
        #endregion

        public XOrderBy<TEntity> Order(XOrderByBuilder builder = null)
        {
            if (builder != null && builder.Infos.Count > 0)
            {
                CheckCallFrom();

                OrderByInfos.AddRange(builder.Infos);
            }
            
            return this;
        }

        public new XOrderBy<TEntity> Copy()
        {
            return this.Clone();
        }
    }
}
