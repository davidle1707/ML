using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public static class XBuilder<TEntity>
    {
        #region Filter

        public static XFilterBuilder<TEntity> Filter => new XFilterBuilder<TEntity>();

        #endregion

        #region From

        public static XJoin<TEntity> From(string dbPath, string alias = null) => XFromBuilder<TEntity>.From(dbPath, alias);

        #endregion

        #region FromElem

       public static XJoin<TElem> FromElem<TId, TElem>(string dbPath, Expression<Func<TEntity, TId>> primaryNode, TId primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
            => XFromBuilder<TEntity>.FromElem(dbPath, primaryNode, primaryValue, elemsNode, alias);

       public static XJoin<TElem> FromElem<TId, TElem>(string dbPath, Expression<Func<TEntity, TId>> primaryNode, IEnumerable<TId> primaryValues, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
            => XFromBuilder<TEntity>.FromElem(dbPath, primaryNode, primaryValues, elemsNode, alias);

        #endregion

        #region NodeFunc

        public static XNodeFunc<TEntity> NodeFunc => new XNodeFunc<TEntity>();

        #endregion

        #region UpdateField

        public static XUpdateFieldBuilder<TEntity> UpdateField => new XUpdateFieldBuilder<TEntity>();

        #endregion

        #region Join
        
        public static XJoinBuilder<TEntity> Join => new XJoinBuilder<TEntity>();

        #endregion

        #region Select

        public static XSelectBuilder<TEntity> Select => new XSelectBuilder<TEntity>();

        #endregion

        #region OrderBy

        public static XOrderByBuilder<TEntity> OrderBy => new XOrderByBuilder<TEntity>();

        #endregion
    }

    public static class XBuilder
    {
        #region QueryMultiple

        public static XQueryMultipleBuilder QueryMultiple(bool ignoreDeclareNamespace = true) => new XQueryMultipleBuilder(ignoreDeclareNamespace);

        #endregion

        #region FileHelper

        public static XFileHelper FileHelper(string rootPath = "XQuery") => new XFileHelper(rootPath);

        #endregion
    }
}
