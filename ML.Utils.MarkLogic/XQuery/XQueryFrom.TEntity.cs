using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQueryFrom<TEntity> : XQueryJoin<TEntity>
	{
		public XQueryFrom(string dbPath)
			: base(dbPath)
		{

		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TEntity> From(string alias = null)
		{
			if (FromInfo.IsValid)
			{
				throw new Exception("Only call From<TEntity> one time.");
			}

			FromInfo.Attribute = MLUtils.GetAttribute<TEntity>();
			FromInfo.Alias = MLUtils.Alias<TEntity>(alias, FromInfo.Attribute);
			FromInfo.Document = Document<TEntity>(FromInfo.Attribute);

			return this;
		}

		#region FromInList

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TChild> FromInList<TChild>(Expression<Func<TEntity, object>> primaryNode, Guid primaryValue, Expression<Func<TEntity, IList<TChild>>> colList, string alias = null)
		{
			var condition = XBuilder<TEntity>.Filter.Guid(primaryNode, primaryValue, "=", string.Empty);

			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TChild> FromInList<TChild>(Expression<Func<TEntity, object>> primaryNode, IEnumerable<Guid> primaryValues, Expression<Func<TEntity, IList<TChild>>> colList, string alias = null)
		{
			var condition = XBuilder<TEntity>.Filter.In(primaryNode, primaryValues.ToList(), alias = string.Empty);

			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TChild> FromInList<TChild>(string condition, Expression<Func<TEntity, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, new[] { condition });
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TChild> FromInList<TChild>(IEnumerable<string> conditions, Expression<Func<TEntity, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, conditions);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TChild> FromInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, string alias = null)
		{
			return ProcessFromInList(colList, alias, null);
		}

		private XQueryJoin<TChild> ProcessFromInList<TChild>(Expression<Func<TEntity, IList<TChild>>> colList, string alias, IEnumerable<string> conditions)
		{
			if (FromInfo.IsValid)
			{
				throw new Exception("Only call From<TEntity> one time.");
			}

		    var fromChild = new XQueryFrom<TChild>(DbPath)
		    {
		        FromInfo =
		        {
		            Attribute = MLUtils.GetAttribute<TEntity>(),
		            Alias = MLUtils.Alias<TEntity>(alias, FromInfo.Attribute),
		            Document = DocumentInList(colList, FromInfo.Attribute, conditions)
		        }
		    };

		    return fromChild;
		}

		#endregion

		public new XQueryFrom<TEntity> Copy()
		{
			return this.Clone();
		}
	}
}
