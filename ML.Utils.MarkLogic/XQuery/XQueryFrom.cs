using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQueryFrom : XQueryJoin
	{
		public XQueryFrom(string dbPath)
			: base(dbPath)
		{

		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin From<TFrom>(string alias = null)
		{
			if (FromInfo.IsValid)
			{
				throw new Exception("Only call From<TFrom> one time.");
			}

			FromInfo.Attribute = MLUtils.GetAttribute<TFrom>();
			FromInfo.Alias = MLUtils.Alias<TFrom>(alias, FromInfo.Attribute);
			FromInfo.Document = MLUtils.GetDocumentQuery<TFrom>(FromInfo.Attribute);

			return this;
		}

		#region FromInList

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin FromInList<TFrom, TChild>(Expression<Func<TFrom, object>> primaryNode, Guid primaryValue, Expression<Func<TFrom, IList<TChild>>> colList, string alias = null)
		{
			var condition = Filter<TFrom>().Guid(primaryNode, primaryValue, "=", string.Empty);

			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin FromInList<TFrom, TChild>(Expression<Func<TFrom, object>> primaryNode, IEnumerable<Guid> primaryValues, Expression<Func<TFrom, IList<TChild>>> colList, string alias = null)
		{
			var condition = Filter<TFrom>().In(primaryNode, primaryValues.ToList(), string.Empty);

			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin FromInList<TFrom, TChild>(string condition, Expression<Func<TFrom, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin FromInList<TFrom, TChild>(IEnumerable<string> conditions, Expression<Func<TFrom, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, conditions);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin FromInList<TFrom, TChild>(Expression<Func<TFrom, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, null);
		}

		private XQueryJoin ProcessFromInList<TFrom, TChild>(Expression<Func<TFrom, IList<TChild>>> colList, string alias, IEnumerable<string> conditions)
		{
			if (FromInfo.IsValid)
			{
				throw new Exception("Only call From<TFrom> one time.");
			}

			FromInfo.Attribute = MLUtils.GetAttribute<TFrom>();
			FromInfo.Alias = MLUtils.Alias<TFrom>(alias, FromInfo.Attribute);
			FromInfo.Document = MLUtils.GetDocumentInListQuery<TFrom, TChild>(colList, FromInfo.Attribute, conditions);

			return this;
		}

		#endregion

		public new XQueryFrom Copy()
		{
			return this.Clone();
		}
	}
}
