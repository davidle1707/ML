using ML.Common;
using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XJoin<TEntity> : XWhere<TEntity>
    {
        internal XJoin(string dbPath)
            : base(dbPath)
        {
        }

        public XJoin<TEntity> InnerJoin<TJoin>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TEntity, object>> colFrom,  string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            return Join(XBuilder<TEntity>.Join.Inner<TJoin>(colJoin, colFrom, aliasJoin, aliasFrom, filter));
        }

        public XJoin<TEntity> InnerJoin<TJoin, TFrom>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TFrom, object>> colFrom,  string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            return Join(XBuilder<TFrom>.Join.Inner<TJoin>(colJoin, colFrom, aliasJoin, aliasFrom, filter));
        }

        public XJoin<TEntity> InnerJoin<TJoin>(string condition, string alias = null)
        {
            return Join(XBuilder<TEntity>.Join.Inner<TJoin>(condition, alias));
        }

        public XJoin<TEntity> LeftJoin<TJoin>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TEntity, object>> colFrom, string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            return Join(XBuilder<TEntity>.Join.Left<TJoin>(colJoin, colFrom, aliasJoin, aliasFrom, filter));
        }

        public XJoin<TEntity> LeftJoin<TJoin, TFrom>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TFrom, object>> colFrom, string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            return Join(XBuilder<TFrom>.Join.Left<TJoin>(colJoin, colFrom, aliasJoin, aliasFrom, filter));
        }

        public XJoin<TEntity> LeftJoin<TJoin>(string condition, string alias = null)
        {
            return Join(XBuilder<TEntity>.Join.Left<TJoin>(condition, alias));
        }
        
        public XJoin<TEntity> Join(XJoinBuilder builder = null)
        {
            if (builder != null && builder.Infos.Count > 0)
            {
                CheckCallFrom();

                JoinInfos.AddRange(builder.Infos);
            }

            return this;
        }

        public new XJoin<TEntity> Copy()
        {
            return this.Clone();
        }
    }
}
