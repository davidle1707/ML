using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.MarkLogic
{
    public abstract class XEntityNode<T, TId> where T : XEntity<TId>
    {
        #region Count

        public abstract int Count(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract Task<int> CountAsync(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract int Count(XFilter<T> filter);

        public abstract Task<int> CountAsync(XFilter<T> filter);

        #endregion

        #region Delete

        public abstract bool Delete(TId primaryValue);

        public abstract Task<bool> DeleteAsync(TId primaryValue);

        public abstract bool DeleteElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue);

        public abstract Task<bool> DeleteElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue);

        #endregion

        #region FindMany

        public abstract List<T> FindMany(XFilter<T> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null);

        public abstract Task<List<T>> FindManyAsync(XFilter<T> filter = null, XOrderByBuilder orderBy = null, XSelectBuilder select = null);

        public abstract List<T> FindMany(Func<XFilterBuilder<T>, XFilter<T>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null);

        public abstract Task<List<T>> FindManyAsync(Func<XFilterBuilder<T>, XFilter<T>> filter, XOrderByBuilder orderBy = null, XSelectBuilder select = null);

        public abstract ExecuteResponse<T> FindMany(string query);

        public abstract Task<ExecuteResponse<T>> FindManyAsync(string query);

        #endregion

        #region FindOne

        public abstract T FindOne(TId primaryValue);

        public abstract Task<T> FindOneAsync(TId primaryValue);

        public abstract T FindOne(XFilter<T> filter = null);

        public abstract Task<T> FindOneAsync(XFilter<T> filter = null);

        public abstract T FindOne(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract Task<T> FindOneAsync(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract T FindOne(string query);

        public abstract Task<T> FindOneAsync(string query);

        #endregion

        #region From

        public abstract XJoin<T> From(string alias = null);

        public abstract XJoin<TElem> FromElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null);

        public abstract XJoin<TElem> FromElem<TElem>(IEnumerable<TId> primaryValues, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null);

        #endregion

        #region Insert

        public abstract bool Insert(T source);

        public abstract Task<bool> InsertAsync(T source);

        public abstract bool InsertMany(List<T> sources);

        public abstract Task<bool> InsertManyAsync(List<T> sources);

        public abstract bool InsertField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        public abstract Task<bool> InsertFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        public abstract bool InsertElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, TElem elem);

        public abstract Task<bool> InsertElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, TElem elem);

        #endregion

        #region IsExists

        public abstract bool IsExists(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract Task<bool> IsExistsAsync(Func<XFilterBuilder<T>, XFilter<T>> builder);

        public abstract bool IsExists(XFilter<T> filter);

        public abstract Task<bool> IsExistsAsync(XFilter<T> filter);

        public abstract bool IsExists<TField, TIgnore>(Expression<Func<T, TField>> col, TField value, Expression<Func<T, TIgnore>> ignoreCol, TIgnore ignoreValue);

        public abstract Task<bool> IsExistsAsync<TField, TIgnore>(Expression<Func<T, TField>> col, TField value, Expression<Func<T, TIgnore>> ignoreCol, TIgnore ignoreValue);

        public abstract bool IsFieldExists<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode);

        public abstract Task<bool> IsFieldExistsAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode);

        public abstract bool IsExists<TField>(Expression<Func<T, TField>> col, TField value);

        public abstract Task<bool> IsExistsAsync<TField>(Expression<Func<T, TField>> col, TField value);

        #endregion

        #region Save

        public abstract bool Save(bool isNew, T source);

        public abstract Task<bool> SaveAsync(bool isNew, T source);

        public abstract bool Save(T source);

        public abstract Task<bool> SaveAsync(T source);

        public abstract bool SaveField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        public abstract Task<bool> SaveFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        #endregion

        #region Update

        public abstract bool Update(T source);

        public abstract Task<bool> UpdateAsync(T source);

        public abstract bool UpdateMany(List<T> sources);

        public abstract Task<bool> UpdateManyAsync(List<T> sources);

        public abstract bool UpdateField<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        public abstract Task<bool> UpdateFieldAsync<TField>(TId primaryValue, Expression<Func<T, TField>> fieldNode, TField fieldValue);

        public abstract bool UpdateField(TId primaryValue, XUpdateFieldBuilder<T> builder);

        public abstract Task<bool> UpdateFieldAsync(TId primaryValue, XUpdateFieldBuilder<T> builder);

        public abstract bool UpdateElem<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem);

        public abstract Task<bool> UpdateElemAsync<TElem>(TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem);

        #endregion

    }
}
