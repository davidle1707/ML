using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQueryJoin<TEntity> : XQueryWhere<TEntity>
	{
		public XQueryJoin(string dbPath)
			: base(dbPath)
		{ }

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TEntity> InnerJoin<TJoin>(Expression<Func<TEntity, object>> colEntity, Expression<Func<TJoin, object>> colJoin,  string aliasEntity = null, string aliasJoin = null)
		{
			var condition = ((PropertyInfo)std.MemberInfo(colEntity)).PropertyType == typeof(string)
				? XBuilder<TJoin>.Filter.String(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity)
				: XBuilder<TJoin>.Filter.StringExactly(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity);

			return InnerJoin<TJoin>(condition, aliasJoin);
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TEntity> InnerJoin<TJoin>(string condition, string alias = null)
		{
			CheckValid();

			var info = new QueryJoinInfo() { Attribute = MLUtils.GetAttribute<TJoin>(), Condition = condition };
			info.Alias = MLUtils.Alias<TJoin>(alias, info.Attribute);
			info.Document = Document<TJoin>(info.Attribute);

			JoinInfos.Add(info);

			return this;
		}

		/// <summary>
		/// if alias is null, use entity name as alias
		/// </summary>
		public XQueryJoin<TEntity> LeftJoin<TJoin>(Expression<Func<TEntity, object>> colEntity, Expression<Func<TJoin, object>> colJoin, string aliasEntity = null, string aliasJoin = null, XQueryWhere<TEntity> joinWhere = null)
		{
			var condition = ((PropertyInfo)std.MemberInfo(colEntity)).PropertyType == typeof(string)
				? XBuilder<TJoin>.Filter.String(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity)
				: XBuilder<TJoin>.Filter.StringExactly(colJoin, colEntity, alias: string.Empty, alias2: aliasEntity);

			if (joinWhere != null)
			{
				var aliasTemp = $"${MLUtils.GetAttribute<TJoin>().EntityName}/";
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
		public XQueryJoin<TEntity> LeftJoin<TJoin>(string condition, string alias = null)
		{
			CheckValid();

			var info = new QueryJoinInfo(false) { Attribute = MLUtils.GetAttribute<TJoin>(), Condition = condition };
			info.Alias = MLUtils.Alias<TJoin>(alias, info.Attribute);
			info.Document = Document<TJoin>(info.Attribute);

			JoinInfos.Add(info);

			return this;
		}

		public new XQueryJoin<TEntity> Copy()
		{
			return this.Clone();
		}
	}
}
