using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public partial class MarkLogicManager
    {
        #region IsExists

        public bool IsExists<TEntity>(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return IsExists(XExtensions.GetFilter(builder));
        }

        public bool IsExists<TEntity>(XFilter<TEntity> filter)
        {
            return Count(filter) > 0;
        }

        public bool IsExists<TEntity, TField, TIgnore>(Expression<Func<TEntity, TField>> col, TField value, Expression<Func<TEntity, TIgnore>> ignoreCol, TIgnore ignoreValue)
        {
            var bf = XBuilder<TEntity>.Filter;
            var query = XBuilder<TEntity>.From(DbPath).Where(bf.Eq(col, value) & bf.NotEq(ignoreCol, ignoreValue)).Count();

            var count = Scalar(query);

            return count.ToInt() > 0;
        }

        public bool IsFieldExists<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode)
        {
            var query = Query<TEntity>().IsFieldExists(primaryValue, fieldNode);

            var execute = ExecuteToString(query);

            return execute.ToInt() > 0;
        }

        public bool IsExists<TEntity, TField>(Expression<Func<TEntity, TField>> col, TField value)
        {
            var query = Query<TEntity>().IsExists(col, value);

            var execute = ExecuteToString(query);

            return execute.ToInt() > 0;
        }

        #endregion

        #region Count

        public int Count<TEntity>(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return Count(XExtensions.GetFilter(builder));
        }

        public int Count<TEntity>(XFilter<TEntity> filter)
        {
            var query = From<TEntity>().Where(filter).Count();

            return Scalar(query).ToInt();
        }

        #endregion

        #region Insert

        public bool Insert<TEntity>(TEntity source, Expression<Func<TEntity, object>> primary)
        {
            var query = Query<TEntity>().Insert(source, primary);

            ExecuteToString(query);

            return true;
        }

        public bool InsertMany<TEntity>(List<TEntity> sources, Expression<Func<TEntity, object>> primary)
        {
            var query = Query<TEntity>().InsertMany(sources, primary);

            ExecuteToString(query);

            return true;
        }

        public bool InsertField<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            var query = Query<TEntity>().InsertField(primaryValue, fieldNode, fieldValue);

            ExecuteToString(query);

            return true;
        }

        public bool InsertElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            var query = Query<TEntity>().InsertElem(primaryValue, elemsNode, elem);

            ExecuteToString(query);

            return true;
        }

        #endregion

        #region Update
        
        public bool Update<TEntity, TPrimary>(TEntity source, Expression<Func<TEntity, TPrimary>> primary)
        {
            var query = Query<TEntity>().Update(source, primary);

            ExecuteToString(query);

            return true;
        }

        public bool UpdateMany<TEntity, TPrimary>(List<TEntity> sources, Expression<Func<TEntity, TPrimary>> primary)
        {
            var query = Query<TEntity>().UpdateMany(sources, primary);

            ExecuteToString(query);

            return true;
        }

        public bool UpdateField<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            var query = Query<TEntity>().UpdateField(primaryValue, fieldNode, fieldValue);

            ExecuteToString(query);

            return true;
        }

        public bool UpdateField<TEntity>(object primaryValue, XUpdateFieldBuilder<TEntity> builder)
        {
            var fields = builder.Fields;

            if (fields.Length == 0)
            {
                return false;
            }

            var query = Query<TEntity>().UpdateFields(primaryValue, fields);

            ExecuteToString(query);

            return true;
        }
        
        public bool UpdateElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            var query = Query<TEntity>().UpdateElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue, elem);

            ExecuteToString(query);

            return true;
        }

        #endregion

        #region Save

        public bool Save<TEntity>(bool isNew, TEntity source, Expression<Func<TEntity, object>> primary)
        {
            return isNew ? Insert(source, primary) : Update(source, primary);
        }

        public bool Save<TEntity>(TEntity source, Expression<Func<TEntity, object>> primary)
        {
            var primaryValue = primary.Compile()(source);

            return IsExists(primary, primaryValue) ? Update(source, primary) : Insert(source, primary);
        }

        public bool SaveField<TEntity, TPrimary, TField>(Expression<Func<TEntity, TPrimary>> primaryNode, TPrimary primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return IsFieldExists(primaryValue, fieldNode) ? UpdateField(primaryValue, fieldNode, fieldValue) : InsertField(primaryValue, fieldNode, fieldValue);
        }

        #endregion

        #region Delete
        
        public bool Delete<TEntity>(object id)
        {
            var query = Query<TEntity>().Delete(id);

            ExecuteToString(query);

            return true;
        }

        public bool DeleteElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            var query = Query<TEntity>().DeleteElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue);

            ExecuteToString(query);

            return true;
        }

        #endregion

        #region IsInUse

        public bool IsInUse(object value, params string[] ignoreEntities)
        {
            var query = XNodeFunc.IsInUse(value);

            var execute = ExecuteToString(query);

            return execute.ToInt() > 1;
        }

        #endregion

        #region FindOne

        public TEntity FindOne<TEntity, TPrimary>(Expression<Func<TEntity, TPrimary>> col, TPrimary value) where TEntity : class
        {
            return FindOne<TEntity>(b => b.Eq(col, value));
        }

        public TEntity FindOne<TEntity>(XFilter<TEntity> filter = null) where TEntity : class
        {
            return FindOne<TEntity>(b => filter);
        }

        public TEntity FindOne<TEntity>(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder) where TEntity : class
        {
            var query = From<TEntity>().ToFirst(builder);

            return FindOne<TEntity>(query);
        }

        public TEntity FindOne<TEntity>(string query) where TEntity : class
        {
            return FindMany<TEntity>(query).FirstOrDefault();
        }
        
        #endregion

        #region FindMany

        public List<TEntity> FindMany<TEntity>(XFilter<TEntity> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null) where TEntity : class
        {
            return FindMany<TEntity>(b => filter, orderBy, select);
        }

        public List<TEntity> FindMany<TEntity>(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null) where TEntity : class
        {
            var query = From<TEntity>().ToList(filter, orderBy, select);

            return FindMany<TEntity>(query).Items;
        }

        public ExecuteResponse<TEntity> FindMany<TEntity>(string query) where TEntity : class
        {
            return ExecuteToObject<ExecuteResponse<TEntity>>(query);
        }

        #endregion

        #region Scalar

        public string Scalar(string query)
        {
            return ExecuteToObject<ExecuteScalarResponse>(query).Value;
        }

        #endregion

        #region Func

        public ExecuteResponse<T> GetsByFunc<T>(string funcName, bool transaction, params object[] paramValues) where T : class
        {
            return ExecuteFuncToObject<ExecuteResponse<T>>(funcName, transaction, paramValues);
        }

        #endregion
        
        #region File

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value)
        /// </summary>
        protected ExecuteResponse<T> GetsByFile<T>(string filePath, Dictionary<string, string> @params = null) where T : class
        {
            return ExecuteFileToObject<ExecuteResponse<T>>(filePath, @params);
        }

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value, use XQueryHelper<TEntity>().ParamValue to convert value)
        /// </summary>
        protected ExecuteResponse<T> GetsByFile<T>(string filePath, Dictionary<string, object> @params = null) where T : class
        {
            return ExecuteFileToObject<ExecuteResponse<T>>(filePath, @params);
        }

        protected ExecuteResponse<T> GetsByFile<T>(string filePath, params string[] paramValues) where T : class
        {
            return ExecuteFileToObject<ExecuteResponse<T>>(filePath, paramValues);
        }

        /// <summary>
        /// use MLUtls.ParamValue to convert value
        /// </summary>
        protected ExecuteResponse<T> GetsByFile<T>(string filePath, params object[] paramValues) where T : class
        {
            return ExecuteFileToObject<ExecuteResponse<T>>(filePath, paramValues);
        }

        #endregion

        #region From

        public XJoin<TEntity> From<TEntity>(string alias = null)
        {
            return XBuilder<TEntity>.From(DbPath, alias);
        }

        public XJoin<TElem> FromElem<TEntity, TElem>(Expression<Func<TEntity, object>> primaryNode, object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
        {
            return XBuilder<TEntity>.FromElem(DbPath, primaryNode, primaryValue, elemsNode, alias);
        }

        public XJoin<TElem> FromElem<TEntity, TElem>(Expression<Func<TEntity, object>> primaryNode, IEnumerable<object> primaryValues, Expression<Func<TEntity, IList<TElem>>> elemsNode, string alias = null)
        {
            return XBuilder<TEntity>.FromElem(DbPath, primaryNode, primaryValues, elemsNode, alias);
        }

        #endregion
    }
}
