using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class MarkLogicManager<TEntity> : MarkLogicManager where TEntity : class
	{
	    public XFilterBuilder<TEntity> FilterBuilder => XBuilder<TEntity>.Filter;

        public XFilter<TOther> Filter<TOther>(Func<XFilterBuilder<TOther>, XFilter<TOther>> builder) => builder(XBuilder<TOther>.Filter);

        public XNodeFunc<TEntity> NodeFunc => new XNodeFunc<TEntity>();

        public MarkLogicManager(string connectionString, string dbPath, Type logType = null)
			: base(connectionString, dbPath, logType)
		{
        }

        #region IsExists

        public bool IsExists(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return base.IsExists(builder);
        }

        public bool IsExists(XFilter<TEntity> filter)
        {
            return base.IsExists(filter);
        }

        public bool IsExists<TPrimary>(Expression<Func<TEntity, TPrimary>> col, TPrimary value)
        {
            return base.IsExists(col, value);
        }

        public bool IsExists<TField, TIgnore>(Expression<Func<TEntity, TField>> col, TField value, Expression<Func<TEntity, TIgnore>> ignoreCol, TIgnore ignoreValue)
        {
            return base.IsExists(col, value, ignoreCol, ignoreValue);
        }

        public bool IsFieldExists<TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode)
        {
            return base.IsFieldExists(primaryValue, fieldNode);
        }

        #endregion

        #region Count
        
        public int Count(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return base.Count(builder);
        }

        public int Count(XFilter<TEntity> filter)
        {
            return base.Count(filter);
        }

        #endregion

        #region Insert

        public bool Insert(TEntity source, Expression<Func<TEntity, object>> primary)
        {
            return base.Insert(source, primary);
        }

        public bool InsertField<TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return base.InsertField(primaryValue, fieldNode, fieldValue);
        }

        public bool InsertElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            return base.InsertElem(primaryValue, elemsNode, elem);
        }

        public bool InsertMany(List<TEntity> sources, Expression<Func<TEntity, object>> primary)
        {
            return base.InsertMany(sources, primary);
        }

        #endregion

        #region Update

        public bool Update<TPrimary>(Expression<Func<TEntity, TPrimary>> primary, TEntity source)
        {
            return base.Update(source, primary);
        }

        public bool UpdateMany<TPrimary>(List<TEntity> sources, Expression<Func<TEntity, TPrimary>> primary)
        {
            return base.UpdateMany(sources, primary);
        }

        public bool UpdateField<TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return base.UpdateField(primaryValue, fieldNode, fieldValue);
        }

        public bool UpdateField(object primaryValue, XUpdateFieldBuilder<TEntity> builder)
        {
            return base.UpdateField<TEntity>(primaryValue, builder);
        }
        
        public bool UpdateElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            return base.UpdateElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue, elem);
        }

        #endregion

        #region Save

        public bool Save(bool isNew, TEntity source, Expression<Func<TEntity, object>> primary)
        {
            return isNew ? Insert(source, primary) : Update(source, primary);
        }

        public bool Save(TEntity source, Expression<Func<TEntity, object>> primary)
        {
            return base.Save(source, primary);
        }

        public bool SaveField<TPrimary, TField>(Expression<Func<TEntity, TPrimary>> primaryNode, TPrimary primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return base.SaveField(primaryNode, primaryValue, fieldNode, fieldValue);
        }

        #endregion

        #region FindOne

        public TEntity FindOne<TPrimary>(Expression<Func<TEntity, TPrimary>> col, TPrimary value)
        {
            return base.FindOne<TEntity, TPrimary>(col, value);
        }

        public TEntity FindOne(XFilter<TEntity> filter = null)
        {
            return base.FindOne<TEntity>(filter);
        }

        public TEntity FindOne(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return base.FindOne<TEntity>(builder);
        }
        
        #endregion

        #region FindMany

        public List<TEntity> FindMany(XFilter<TEntity> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return base.FindMany<TEntity>(filter, orderBy, select);
        }

        public List<TEntity> FindMany(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder, XOrderByBuilder orderBy = null, XSelectBuilder select = null)
        {
            return base.FindMany<TEntity>(builder, orderBy, select);
        }
        
        #endregion

        #region Delete

        public bool Delete(object id)
        {
            return base.Delete<TEntity>(id);
        }

        public bool DeleteElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            return base.DeleteElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue);
        }

        #endregion

        #region From
        
        public XJoin<TEntity> From(string alias = null)
        {
            return base.From<TEntity>(alias);
        }

        public XJoin<TElem> FromElem<TElem>(Expression<Func<TEntity, object>> primaryNode, object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
        {
            return base.FromElem<TEntity, TElem>(primaryNode, primaryValue, elemsNode, alias);
        }

        public XJoin<TElem> FromElem<TElem>(Expression<Func<TEntity, object>> primaryNode, IEnumerable<object> primaryValues, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
        {
            return base.FromElem<TEntity, TElem>(primaryNode, primaryValues, elemsNode, alias);
        }

        #endregion
    }
}
