using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQueryJoin : XQueryWhere
	{
		public XQueryJoin(string dbPath)
			: base(dbPath)
		{ }

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin InnerJoin<TJoin, TEntity>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TEntity, object>> colEntity, string aliasJoin = null, string aliasEntity = null)
		{
			var condition = ((PropertyInfo)std.MemberInfo(colEntity)).PropertyType == typeof(string)
				? Filter<TJoin>().String(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity)
				: Filter<TJoin>().StringExactly(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity);

			return InnerJoin<TJoin>(condition, aliasJoin);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin InnerJoin<TJoin>(string condition, string alias = null)
		{
			CheckValid();

			var info = new QueryJoinInfo() { Attribute = MLUtils.GetAttribute<TJoin>(), Condition = condition };
			info.Alias = MLUtils.Alias<TJoin>(alias, info.Attribute);
			info.Document = MLUtils.GetDocumentQuery<TJoin>(info.Attribute);

			JoinInfos.Add(info);

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin LeftJoin<TJoin, TEntity>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TEntity, object>> colEntity, string aliasJoin = null, string aliasEntity = null, XQueryWhere joinWhere = null)
		{
			var condition = ((PropertyInfo)std.MemberInfo(colEntity)).PropertyType == typeof(string)
				? Filter<TJoin>().String(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity)
				: Filter<TJoin>().StringExactly(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity);

			if (joinWhere != null)
			{
				var aliasTemp = $"{"$"}{MLUtils.GetAttribute<TJoin>().EntityName}{"/"}";
				var sb = new StringBuilder();
				foreach (var item in joinWhere.QueryWheres)
				{
					sb.Append(" and ");
					sb.Append(item.Contains("/") ? item.Replace(aliasTemp, "") : item);
				}
				condition += sb.ToString();
			}

			return LeftJoin<TJoin>(condition, aliasJoin);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin LeftJoin<TJoin>(string condition, string alias = null)
		{
			CheckValid();

			var info = new QueryJoinInfo(false) { Attribute = MLUtils.GetAttribute<TJoin>(), Condition = condition };
			info.Alias = MLUtils.Alias<TJoin>(alias, info.Attribute);
			info.Document = MLUtils.GetDocumentQuery<TJoin>(info.Attribute);

			JoinInfos.Add(info);

			return this;
		}

		public new XQueryJoin Copy()
		{
			return this.Clone();
		}
	}
}
