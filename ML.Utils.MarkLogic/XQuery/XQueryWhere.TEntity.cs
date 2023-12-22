using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
    [Serializable]
	public class XQueryWhere<TEntity> : XQueryOrderBy
	{
		public XQueryWhere(string dbPath)
			: base(dbPath)
		{
			
		}

		/// <summary>
		/// 1=1
		/// </summary>
		public XQueryWhere<TEntity> WhereDefault()
		{
			return Where("1=1");
		}

		public XQueryWhere<TEntity> Where(string condition)
		{
			if (!string.IsNullOrEmpty(condition))
			{
				QueryWheres.Add(condition);
			}

			return this;
		}

        public XQueryWhere<TEntity> Where(IEnumerable<string> conditions)
        {
            QueryWheres.AddRange(conditions.Where(c => !string.IsNullOrEmpty(c)));

            return this;
        }

		public XQueryWhere<TEntity> WhereOr(params string[] orConditions)
		{
			if (orConditions != null && orConditions.Length > 0)
			{
				QueryWheres.Add("(" + string.Join(" or ", orConditions) + ")");
			}

			return this;
		}

		public XQueryWhere<TEntity> WhereCompare<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.Operator(col, col2, @operator, alias, alias2));

			return this;
		}

		#region String

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereStringExactly(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.StringExactly(col, value, @operator, alias));

			return this;
		}

        /// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereString(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
        {
            return WhereStringT(col, value, @operator, alias);
        }

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereStringT<T>(Expression<Func<T, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(value != null && value.SafeType() == typeof(string)
				? XBuilder<T>.Filter.String(col, value, @operator, alias)
				: XBuilder<T>.Filter.StringExactly(col, value, @operator, alias)
			);

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereString<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.String(col, col2, @operator, alias, alias2));

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereStringContains(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.StringContains(col, value, @operator, alias));

			return this;
		}

        public XQueryWhere<TEntity> WhereStringExactlyInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, object value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(XBuilder<TEntity>.Filter.StringExactlyInList(colList, colItem, value, @operator, alias));

            return this;
        }

        public XQueryWhere<TEntity> WhereStringInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, object value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(value != null && value.SafeType() == typeof(string)
                ? XBuilder<TEntity>.Filter.StringInList(colList, colItem, value, @operator, alias)
                 : XBuilder<TEntity>.Filter.StringExactlyInList(colList, colItem, value, @operator, alias)
             );

            return this;
        }

		#endregion

		#region Guid

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereGuid(Expression<Func<TEntity, object>> col, Guid? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Guid(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereGuid<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.Guid(col, col2, @operator, alias, alias2));

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereGuidInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, Guid? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(XBuilder<TEntity>.Filter.GuidInList(colList, colItem, value, @operator, alias));

            return this;
        }

		#endregion

		#region Date

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDateTimeRange(Expression<Func<TEntity, object>> col, DateTime? fromDate, DateTime? toDate, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.DateTimeRange(col, fromDate, toDate, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDateTime(Expression<Func<TEntity, object>> col, DateTime? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.DateTime(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDateTime<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.DateTime(col, col2, @operator, alias, alias2);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDate(Expression<Func<TEntity, object>> col, DateTime? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Date(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDate<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Date(col, col2, @operator, alias, alias2);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereDateTimeRangeInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, DateTime? fromDate, DateTime? toDate, string alias = null)
        {
            CheckValid();

            var where = XBuilder<TEntity>.Filter.DateTimeRangeInList(colList, colItem, fromDate, toDate, alias);

            if (!string.IsNullOrEmpty(where))
            {
                QueryWheres.Add(where);
            }

            return this;
        }

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereDateTimeInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, DateTime? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            var where = XBuilder<TEntity>.Filter.DateTimeInList(colList, colItem, value, @operator, alias);

            if (!string.IsNullOrEmpty(where))
            {
                QueryWheres.Add(where);
            }

            return this;
        }

		#endregion

		#region Decimal

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDecimal(Expression<Func<TEntity, object>> col, decimal? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Decimal(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereDecimal<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.Decimal(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Short

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereShort(Expression<Func<TEntity, object>> col, short? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Short(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereShort<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.Short(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Int

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereInt(Expression<Func<TEntity, object>> col, int? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Int(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereInt<TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.Int(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Bool

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereBool(Expression<Func<TEntity, object>> col, bool? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.Bool(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereBoolInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, bool? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(XBuilder<TEntity>.Filter.BoolInList(colList, colItem, value, @operator, alias));

            return this;
        }

		#endregion

		#region Null

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereNotNull(Expression<Func<TEntity, object>> col, string alias = null)
		{
			return WhereNullOrNotNull(col, true, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereNull(Expression<Func<TEntity, object>> col, string alias = null)
		{
			return WhereNullOrNotNull(col, false, alias);
		}

        /// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereNullT<T>(Expression<Func<T, object>> col, string alias = null)
        {
            return WhereNullOrNotNull(col, false, alias);
        }

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        private XQueryWhere<TEntity> WhereNullOrNotNull<T>(Expression<Func<T, object>> col, bool isNotNull, string alias)
        {
            CheckValid();

            var where = XBuilder<T>.Filter.Null(col, isNotNull, alias);

            if (!string.IsNullOrEmpty(where))
            {
                QueryWheres.Add(where);
            }

            return this;
        }

        #endregion

        #region In/NotIn

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere<TEntity> WhereNotIn<TType>(Expression<Func<TEntity, object>> col, List<TType> items, string alias = null)
		{
			return WhereInOrNotIn(col, items, true, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere<TEntity> WhereIn<TType>(Expression<Func<TEntity, object>> col, List<TType> items, string alias = null)
		{
			return WhereInOrNotIn(col, items, false, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		private XQueryWhere<TEntity> WhereInOrNotIn<TType>(Expression<Func<TEntity, object>> col, List<TType> items, bool isNotIn, string alias)
		{
			CheckValid();

			var where = XBuilder<TEntity>.Filter.InOrNotIn(col, items, isNotIn, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		#endregion

		#region Node

		public XQueryWhere<TEntity> WhereNodeExist(Expression<Func<TEntity, object>> col, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.NodeExists(col, true, alias));

			return this;
		}

		public XQueryWhere<TEntity> WhereNodeNotExist(Expression<Func<TEntity, object>> col, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(XBuilder<TEntity>.Filter.NodeExists(col, false, alias));

			return this;
		}

		#endregion

		public new XQueryWhere<TEntity> Copy()
		{
			return this.Clone();
		}
	}
}
