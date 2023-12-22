using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQueryWhere : XQueryOrderBy
	{
		public XQueryWhere(string dbPath)
			: base(dbPath)
		{
			
		}

		/// <summary>
		/// 1=1
		/// </summary>
		public XQueryWhere WhereDefault()
		{
			return Where("1=1");
		}

		public XQueryWhere Where(string condition)
		{
			if (!string.IsNullOrEmpty(condition))
			{
				QueryWheres.Add(condition);
			}

			return this;
		}

        public XQueryWhere Where(IEnumerable<string> conditions)
        {
            QueryWheres.AddRange(conditions.Where(c => !string.IsNullOrEmpty(c)));

            return this;
        }

		public XQueryWhere WhereOr(params string[] orConditions)
		{
			if (orConditions != null && orConditions.Length > 0)
			{
				QueryWheres.Add("(" + string.Join(" or ", orConditions) + ")");
			}

			return this;
		}

		public XQueryWhere WhereCompare<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().Operator(col, col2, @operator, alias, alias2));

			return this;
		}

		#region String

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereStringExactly<TEntity>(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().StringExactly(col, value, @operator, alias));

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereString<TEntity>(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(value != null && value.SafeType() == typeof(string)
				? Filter<TEntity>().String(col, value, @operator, alias)
				: Filter<TEntity>().StringExactly(col, value, @operator, alias)
			);

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereString<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().String(col, col2, @operator, alias, alias2));

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereStringContains<TEntity>(Expression<Func<TEntity, object>> col, object value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().StringContains(col, value, @operator, alias));

			return this;
		}

        public XQueryWhere WhereStringExactlyInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, object value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(Filter<TEntity>().StringExactlyInList(colList, colItem, value, @operator, alias));

            return this;
        }

        public XQueryWhere WhereStringInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, object value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(value != null && value.SafeType() == typeof(string)
                ? Filter<TEntity>().StringInList(colList, colItem, value, @operator, alias)
                 : Filter<TEntity>().StringExactlyInList(colList, colItem, value, @operator, alias)
             );

            return this;
        }

		#endregion

		#region Guid

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereGuid<TEntity>(Expression<Func<TEntity, object>> col, Guid? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Guid(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereGuid<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().Guid(col, col2, @operator, alias, alias2));

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere WhereGuidInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, Guid? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(Filter<TEntity>().GuidInList(colList, colItem, value, @operator, alias));

            return this;
        }

		#endregion

		#region Date

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDateTimeRange<TEntity>(Expression<Func<TEntity, object>> col, DateTime? fromDate, DateTime? toDate, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().DateTimeRange(col, fromDate, toDate, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDateTime<TEntity>(Expression<Func<TEntity, object>> col, DateTime? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().DateTime(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDateTime<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			var where = Filter<TEntity>().DateTime(col, col2, @operator, alias, alias2);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDate<TEntity>(Expression<Func<TEntity, object>> col, DateTime? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Date(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDate<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Date(col, col2, @operator, alias, alias2);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere WhereDateTimeRangeInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, DateTime? fromDate, DateTime? toDate, string alias = null)
        {
            CheckValid();

            var where = Filter<TEntity>().DateTimeRangeInList(colList, colItem, fromDate, toDate, alias);

            if (!string.IsNullOrEmpty(where))
            {
                QueryWheres.Add(where);
            }

            return this;
        }

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere WhereDateTimeInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, DateTime? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            var where = Filter<TEntity>().DateTimeInList(colList, colItem, value, @operator, alias);

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
		public XQueryWhere WhereDecimal<TEntity>(Expression<Func<TEntity, object>> col, decimal? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Decimal(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereDecimal<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().Decimal(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Short

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereShort<TEntity>(Expression<Func<TEntity, object>> col, short? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Short(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereShort<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().Short(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Int

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereInt<TEntity>(Expression<Func<TEntity, object>> col, int? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Int(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereInt<TEntity, TEntity2>(Expression<Func<TEntity, object>> col, Expression<Func<TEntity2, object>> col2, string @operator = XOperator.Eq, string alias = null, string alias2 = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().Int(col, col2, @operator, alias, alias2));

			return this;
		}

		#endregion

		#region Bool

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereBool<TEntity>(Expression<Func<TEntity, object>> col, bool? value, string @operator = XOperator.Eq, string alias = null)
		{
			CheckValid();

			var where = Filter<TEntity>().Bool(col, value, @operator, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryWhere WhereBoolInList<TEntity, TChild>(Expression<Func<TEntity, IList<TChild>>> colList, Expression<Func<TChild, object>> colItem, bool? value, string @operator = XOperator.Eq, string alias = null)
        {
            CheckValid();

            QueryWheres.Add(Filter<TEntity>().BoolInList(colList, colItem, value, @operator, alias));

            return this;
        }

		#endregion

		#region Null

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereNotNull<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
			return WhereNullOrNotNull(col, true, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereNull<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
			return WhereNullOrNotNull(col, false, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		private XQueryWhere WhereNullOrNotNull<TEntity>(Expression<Func<TEntity, object>> col, bool isNotNull, string alias)
		{
			CheckValid();

			var where = Filter<TEntity>().Null(col, isNotNull, alias);

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
		public XQueryWhere WhereNotIn<TEntity, TType>(Expression<Func<TEntity, object>> col, List<TType> items, string alias = null)
		{
			return WhereInOrNotIn(col, items, true, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryWhere WhereIn<TEntity, TType>(Expression<Func<TEntity, object>> col, List<TType> items, string alias = null)
		{
			return WhereInOrNotIn(col, items, false, alias);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		private XQueryWhere WhereInOrNotIn<TEntity, TType>(Expression<Func<TEntity, object>> col, List<TType> items, bool isNotIn, string alias)
		{
			CheckValid();

			var where = Filter<TEntity>().InOrNotIn(col, items, isNotIn, alias);

			if (!string.IsNullOrEmpty(where))
			{
				QueryWheres.Add(where);
			}

			return this;
		}

		#endregion

		#region Node

		public XQueryWhere WhereNodeExist<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().NodeExists(col, true, alias));

			return this;
		}

		public XQueryWhere WhereNodeNotExist<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
			CheckValid();

			QueryWheres.Add(Filter<TEntity>().NodeExists(col, false, alias));

			return this;
		}

		#endregion

		public new XQueryWhere Copy()
		{
			return this.Clone();
		}
	}
}
