using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using ML.Common;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XSelect<TEntity> : XFunction<TEntity>
    {
        internal XSelect(string dbPath)
            : base(dbPath)
        {
        }

        public XSelect<TEntity> SelectEmpty()
        {
            return this;
        }

        #region TEntity

        public XSelect<TEntity> SelectMap<TResult>(Expression<Func<TEntity, object>> colEntity, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            return SelectMap<TEntity, TResult>(colEntity, colResult, aliasEntity);
        }

        public XSelect<TEntity> SelectMapFormat<TResult>(string formatString, List<Expression<Func<TEntity, object>>> colEntities, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            return SelectMapFormat<TEntity, TResult>(formatString, colEntities, colResult, aliasEntity);
        }

        public XSelect<TEntity> Select(Expression<Func<TEntity, object>> col, string alias = null, string nodeName = null)
        {
            return Select<TEntity>(col, alias, nodeName);
        }

        public XSelect<TEntity> SelectChildMap<TMapping, TResult>(Expression<Func<TResult, object>> colResult, List<XChildMap<TEntity, TMapping>> childColumns, string aliasEntity = null)
        {
            return SelectChildMap<TEntity, TMapping, TResult>(colResult, childColumns, aliasEntity);
        }

        public XSelect<TEntity> SelectAllMap<TResult>(string aliasEntity = null)
        {
            return SelectAllMap<TEntity, TResult>(aliasEntity);
        }

        public XSelect<TEntity> SelectAllMap<TResult>(Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            return SelectAllMap<TEntity, TResult>(colResult, aliasEntity);
        }

        public XSelect<TEntity> SelectAll(string alias = null, string nodeName = null)
        {
            return SelectAll<TEntity>(alias, nodeName);
        }

        public XSelect<TEntity> SelectAggregateMap<TResult>(Expression<Func<TEntity, object>> colEntity, XFuncAggregation function, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            return SelectAggregateMap<TEntity, TResult>(colEntity, function, colResult, aliasEntity);
        }

        public XSelect<TEntity> SelectAggregate(Expression<Func<TEntity, object>> col, XFuncAggregation function, string nodeName, string alias = null)
        {
            return SelectAggregate<TEntity>(col, function, nodeName, alias);
        }

        public XSelect<TEntity> SelectElems<TElem>(Expression<Func<TEntity, IList<TElem>>> col, string alias = null)
        {
            return SelectElems<TEntity, TElem>(col, alias);
        }

        public XSelect<TEntity> SelectElem<TElem>(Expression<Func<TEntity, IList<TElem>>> col, Expression<Func<TEntity, TElem>> elem, string alias = null)
        {
            return SelectElem<TEntity, TElem>(col, elem, alias);
        }
        
        #endregion

        #region Generic

        public XSelect<TEntity> SelectMap<T, TResult>(Expression<Func<T, object>> colEntity, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return Select(colEntity, aliasEntity, nodeName);
        }

        public XSelect<TEntity> SelectMapFormat<T, TResult>(string formatString, List<Expression<Func<T, object>>> colEntities, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            var formats = colEntities.Select(colEntity => "{data(" + colEntity.NodeNameWithAlias(aliasEntity) + ")}").ToArray();

            formatString = formatString.Replace(" ", XSpecialCharacter.Space);

            SelectColumns.Add("<" + nodeName + ">" + string.Format(formatString, formats) + "</" + nodeName + ">");

            return this;
        }

        public XSelect<TEntity> Select<T>(Expression<Func<T, object>> col, string alias = null, string nodeName = null)
        {
            return Select(XBuilder<T>.Select.Field(col, alias, nodeName));
        }

        public XSelect<TEntity> SelectChildMap<T, TMap, TResult>(Expression<Func<TResult, object>> colResult, List<XChildMap<T, TMap>> childColumns, string aliasEntity = null)
        {
            return Select(XBuilder<T>.Select.ChildMap(colResult, childColumns, aliasEntity));
        }

        public XSelect<TEntity> SelectAllMap<T, TResult>(string aliasEntity = null)
        {
            return SelectAll<T>(aliasEntity, typeof(TResult).Name);
        }

        public XSelect<TEntity> SelectAllMap<T, TResult>(Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return SelectAll<T>(aliasEntity, nodeName);
        }

        public XSelect<TEntity> SelectAll<T>(string alias = null, string nodeName = null)
        {
            return Select(XBuilder<T>.Select.Fields(alias, nodeName));
        }

        public XSelect<TEntity> Select(string column, string nodeName)
        {
            return Select(XBuilder<TEntity>.Select.Field(column, nodeName));
        }

        public XSelect<TEntity> SelectAggregateMap<T, TResult>(Expression<Func<T, object>> colEntity, XFuncAggregation function, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return SelectAggregate(colEntity, function, nodeName, aliasEntity);
        }

        public XSelect<TEntity> SelectAggregate<T>(Expression<Func<T, object>> col, XFuncAggregation function, string nodeName, string alias = null)
        {
            return Select(XBuilder<T>.Select.Aggregate(col, function, nodeName, alias));
        }

        public XSelect<TEntity> SelectElems<T, TElem>(Expression<Func<T, IList<TElem>>> col, string alias = null)
        {
            return Select(XBuilder<T>.Select.Elems(col, alias));
        }

        public XSelect<TEntity> SelectElem<T, TElem>(Expression<Func<T, IList<TElem>>> col, Expression<Func<T, TElem>> elem, string alias = null)
        {
            return Select(XBuilder<T>.Select.Elem(col, elem, alias));
        }
        
        #endregion
        
        public XSelect<TEntity> Select(XSelectBuilder builder = null)
        {
            if (builder != null && builder.Columns.Count > 0)
            {
                CheckCallFrom();

                SelectColumns.AddRange(builder.Columns);
            }
            
            return this;
        }

        public XSelect<TEntity> Copy()
        {
            return this.Clone();
        }
    }

    
}
