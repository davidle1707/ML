using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class XElemFilterBuilder<TEntity, TElem> : XFilterBuilder<TElem>
    {
        private readonly Expression<Func<TEntity, IList<TElem>>> _colList;
        private readonly string _alias;

        internal XElemFilterBuilder(Expression<Func<TEntity, IList<TElem>>> colList, string alias = null)
        {
            _colList = colList;
            _alias = alias;
        }

        protected override HandleBuilder<TElem, TField> GetHandleBuilder<TField>(Expression<Func<TElem, TField>> col, string alias = null, XFilterOption option = null)
        {
            return new XElemFilterHandleBuilder<TEntity,TElem,TField>(_colList, col, _alias, option);
        }
    }

    public class XFilterBuilder<TEntity>
    {
        internal XFilterBuilder()
        {
        }

        public XFilter<TEntity> Empty => XFilter<TEntity>.Empty;

        public XElemFilterBuilder<TEntity, TElem> Elem<TElem>(Expression<Func<TEntity, IList<TElem>>> colList, string alias = null) => new XElemFilterBuilder<TEntity, TElem>(colList, alias);

        protected virtual HandleBuilder<TEntity, TField> GetHandleBuilder<TField>(Expression<Func<TEntity, TField>> col, string alias = null, XFilterOption option = null)
        {
            return new XFilterHandleBuilder<TEntity, TField>(col, alias, option);
        }

        #region CompareValue

        public XFilter<TEntity> Eq<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "=", value, alias, option);
        }

        public XFilter<TEntity> NotEq<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "!=", value, alias, option);
        }

        public XFilter<TEntity> Gt<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, ">", value, alias, option);
        }

        public XFilter<TEntity> GtOrEq<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, ">=", value, alias, option);
        }

        public XFilter<TEntity> Lt<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "<", value, alias, option);
        }

        public XFilter<TEntity> LtOrEq<TField>(Expression<Func<TEntity, TField>> col, TField value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "<=", value, alias, option);
        }

        public XFilter<TEntity> Contains(Expression<Func<TEntity, string>> col, string value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "Contains", value, alias, option);
        }

        public XFilter<TEntity> StartsWith(Expression<Func<TEntity, string>> col, string value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "StartsWith", value, alias, option);
        }

        public XFilter<TEntity> EndsWith(Expression<Func<TEntity, string>> col, string value, string alias = null, XFilterOption option = null)
        {
            return HandleCompareValue(col, "EndsWith", value, alias, option);
        }
        
        private XFilter<TEntity> HandleCompareValue<TField>(Expression<Func<TEntity, TField>> col, string @operator, TField value, string alias = null, XFilterOption option = null)
        {
            var handler = GetHandleBuilder(col, alias, option);
            handler.CompareValue(value, @operator);

            return handler;
        }
        
        #endregion

        #region CompareField

        public XFilter<TEntity> EqField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, "=", col2, alias, alias2, option);
        }

        public XFilter<TEntity> NotEqField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, "!=", col2, alias, alias2, option);
        }

        public XFilter<TEntity> GtField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, ">", col2, alias, alias2, option);
        }

        public XFilter<TEntity> GtOrEqField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, ">=", col2, alias, alias2, option);
        }

        public XFilter<TEntity> LtField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, "<", col2, alias, alias2, option);
        }

        public XFilter<TEntity> LtOrEqField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            return HandleCompareField(col, "<=", col2, alias, alias2, option);
        }

        private XFilter<TEntity> HandleCompareField<TEntity2, TField>(Expression<Func<TEntity, TField>> col, string @operator, Expression<Func<TEntity2, TField>> col2, string alias = null, string alias2 = null, XFilterOption option = null)
        {
            var handler = GetHandleBuilder(col, alias, option);
            handler.CompareField<TEntity2>(col2, @operator, alias2);

            return handler;
        }

        #endregion

        #region RangeValue

        public XFilter<TEntity> Range<TField>(Expression<Func<TEntity, TField>> col, TField fromValue, TField toValue, string alias = null, XFilterOption option = null)
        {
            return HandleRangeValue(col, fromValue, toValue, alias, option);
        }

        private XFilter<TEntity> HandleRangeValue<TField>(Expression<Func<TEntity, TField>> col, TField formValue, TField toValue, string alias = null, XFilterOption option = null)
        {
            var handler = GetHandleBuilder(col, alias, option);
            handler.RangeValue(formValue, toValue);

            return handler;
        }

        #endregion

        #region In/NotIn

        public XFilter<TEntity> In<TField>(Expression<Func<TEntity, TField>> col, IEnumerable<TField> values, string alias = null, XFilterOption option = null)
        {
            return HandleInOrNotIn(col, false, values, alias, option);
        }

        public XFilter<TEntity> NotIn<TField>(Expression<Func<TEntity, TField>> col, IEnumerable<TField> values, string alias = null, XFilterOption option = null)
        {
            return HandleInOrNotIn(col, true, values, alias, option);
        }

        private XFilter<TEntity> HandleInOrNotIn<TField>(Expression<Func<TEntity, TField>> col, bool isNotIn,  IEnumerable<TField> values, string alias = null, XFilterOption option = null)
        {
            var handler = GetHandleBuilder(col, alias, option);
            handler.InOrNotIn(values, isNotIn);

            return handler;
        }

        #endregion

        #region Null/NotNull

        public XFilter<TEntity> IsNull<TField>(Expression<Func<TEntity, TField>> col, string alias = null)
        {
            return HandleNullOrNotNull(col, false, alias);
        }

        public XFilter<TEntity> IsNotNull<TField>(Expression<Func<TEntity, TField>> col,string alias = null)
        {
            return HandleNullOrNotNull(col, true, alias);
        }

        private XFilter<TEntity> HandleNullOrNotNull<TField>(Expression<Func<TEntity, TField>> col, bool isNotNull, string alias = null)
        {
            var handler = GetHandleBuilder(col, alias, new XFilterOption { LowerCaseIfString = false });
            handler.NullOrNotNull(isNotNull);

            return handler;
        }

        #endregion

        #region Empty/NotEmpty

        public XFilter<TEntity> IsEmpty(Expression<Func<TEntity, string>> col, string alias = null)
        {
            return HandleEmptyOrNotEmpty(col, false, alias);
        }

        public XFilter<TEntity> IsNotEmpty(Expression<Func<TEntity, string>> col, string alias = null)
        {
            return HandleEmptyOrNotEmpty(col, true, alias);
        }

        private XFilter<TEntity> HandleEmptyOrNotEmpty(Expression<Func<TEntity, string>> col, bool isNotEmpty, string alias = null)
        {
            var handler = GetHandleBuilder(col, alias, new XFilterOption { LowerCaseIfString = false });
            handler.EmptyOrNotEmpty(isNotEmpty);

            return handler;
        }

        #endregion

        #region True/False

        public XFilter<TEntity> IsTrue(Expression<Func<TEntity, bool?>> col, string alias = null)
        {
            return HandleTrueOrFalse(col, true, alias);
        }

        public XFilter<TEntity> IsFalse(Expression<Func<TEntity, bool?>> col, string alias = null)
        {
            return HandleTrueOrFalse(col, false, alias);
        }

        private XFilter<TEntity> HandleTrueOrFalse(Expression<Func<TEntity, bool?>> col, bool isTrue, string alias = null)
        {
            return Eq(col, isTrue, alias);
        }

        #endregion

        #region Node Exists/NotExists

        public XFilter<TEntity> IsExists(Expression<Func<TEntity, string>> col, string alias = null)
        {
            return HandleExistsOrNotExistsNode(col, false, alias);
        }

        public XFilter<TEntity> IsNotExists(Expression<Func<TEntity, string>> col, string alias = null)
        {
            return HandleExistsOrNotExistsNode(col, true, alias);
        }

        private XFilter<TEntity> HandleExistsOrNotExistsNode<TField>(Expression<Func<TEntity, TField>> col, bool isNotExists, string alias = null)
        {
            var handler = GetHandleBuilder(col, alias);
            handler.ExistsOrNotExists(isNotExists);

            return handler;    
        }

        #endregion
    }
}
