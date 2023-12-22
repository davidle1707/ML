using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public class XQuerySelect : XQueryFunction
	{
		public XQuerySelect(string dbPath)
			: base(dbPath)
		{

		}

		/// <summary>
		/// Select mapping from entity to result column. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectMapping<TEntity, TResult>(Expression<Func<TEntity, object>> colEntity, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
		{
			var nodeName = std.PropName2(colResult); //only last property name

			return Select(colEntity, aliasEntity, nodeName);

			//SelectColumns.Add("<" + nodeName + ">{data(" + MLUtils.NodeNameWithAlias(colEntity, aliasEntity) + ")}</" + nodeName + ">");
		
			//return this;
		}

		public XQuerySelect SelectFormatMapping<TEntity, TResult>(string formatString, List<Expression<Func<TEntity, object>>> colEntities, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
		{
			var nodeName = std.PropName2(colResult); //only last property name

			var formats = colEntities.Select(colEntity => "{data(" + colEntity.NodeNameWithAlias(aliasEntity) + ")}").ToArray();

		    formatString = formatString.Replace(" ", XSpecialCharacter.Space);

			SelectColumns.Add("<" + nodeName + ">" + string.Format(formatString, formats) + "</" + nodeName + ">");

			return this;
		}

		/// <summary>
		/// Select specify column. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect Select<TEntity>(Expression<Func<TEntity, object>> col, string alias = null, string nodeName = null)
		{
			var prop = (PropertyInfo)std.MemberInfo(col);

			if (prop.PropertyType.IsSimpleType())
			{
				SelectColumns.Add(!string.IsNullOrEmpty(nodeName) ? "<" + nodeName + ">{data(" + col.NodeNameWithAlias(alias) + ")}</" + nodeName + ">" : col.NodeNameWithAlias(alias));
			}
			else
			{
				nodeName = !string.IsNullOrEmpty(nodeName) ? nodeName : prop.Name;

				var allKeyword = $"{MLUtils.GetAttribute<TEntity>().NamespaceVarName}:*";

				SelectColumns.Add(string.Format("<{0}>{{{1}/{2}}}</{0}>", nodeName, col.NodeNameWithAlias(alias), allKeyword));	
			}

			return this;
		}

		/// <summary>
		/// Select specify column. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectChildMapping<TEntity, TMapping, TResult>(Expression<Func<TResult, object>> colResult, List<ChildMapping<TEntity, TMapping>> childColumns, string aliasEntity = null)
		{
			var nodeName = std.PropName2(colResult); //only last property name

			var columns = (from childColumn in childColumns
                           let mappingNodeName = std.PropName2(childColumn.To)
                           select "<" + mappingNodeName + ">{data(" + childColumn.From.NodeNameWithAlias(aliasEntity) + ")}</" + mappingNodeName + ">"
                           ).ToList();

			SelectColumns.Add("<" + nodeName + ">" + string.Join(string.Empty, columns) + "</" + nodeName + ">");

			return this;
		}
		
		/// <summary>
		/// Select all columns. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectAllMapping<TEntity, TResult>(string aliasEntity = null)
		{
			return SelectAll<TEntity>(aliasEntity, typeof(TResult).Name);
		}

		/// <summary>
		/// Select all columns. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectAllMapping<TEntity, TResult>(Expression<Func<TResult, object>> colResult, string aliasEntity = null)
		{
			var nodeName = std.PropName2(colResult); //only last property name

			return SelectAll<TEntity>(aliasEntity, nodeName);
		}

		/// <summary>
		/// Select all columns. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectAll<TEntity>(string alias = null, string nodeName = null)
		{
		    var attribute = MLUtils.GetAttribute<TEntity>();

            var allKeyword = $"{ attribute.NamespaceVarName}:*";

			SelectColumns.Add(!string.IsNullOrEmpty(nodeName)
                ? string.Format("<{0}>{{{1}/{2}}}</{0}>", nodeName, MLUtils.Alias<TEntity>(alias, attribute), allKeyword)
                : MLUtils.Alias<TEntity>(alias, attribute) + "/" + allKeyword);

			return this;
		}
		
		public XQuerySelect Select(string column, string nodeName)
		{
			SelectColumns.Add("<" + nodeName + ">{" + column + "}</" + nodeName + ">");

			return this;
		}

		/// <summary>
		/// Select from LeftJoin. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectByFuncMapping<TEntity, TResult>(Expression<Func<TEntity, object>> colEntity, XFuncAggregation function, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
		{
			var nodeName = std.PropName2(colResult); //only last property name

			return SelectByFunc(colEntity, function, nodeName, aliasEntity);
		}

		/// <summary>
		/// Select from LeftJoin. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectByFunc<TEntity>(Expression<Func<TEntity, object>> col, XFuncAggregation function, string nodeName, string alias = null)
		{
			if (function != XFuncAggregation.None)
			{
				SelectColumns.Add("<" + nodeName + ">{" +
				                  $"{function.ToString().ToLower()}({col.NodeNameWithAlias(alias)})" + "}</" + nodeName + ">");
			}

			return this;
		}

		/// <summary>
		/// Select from LeftJoin. If alias is null, use entity name as alias
		/// </summary>
		public XQuerySelect SelectByFunc<TEntity>(Expression<Func<TEntity, object>> col, XFuncCollection function, string nodeName, string alias = null)
		{
			if (function != XFuncCollection.None)
			{
				//SelectColumns.Add("<" + nodeName + ">{" + string.Format("{0}({1})", function.ToString().ToLower(), MLUtils.NodeNameWithAlias(col, alias)) + "}</" + nodeName + ">");
			}

			return this;
		}

		public XQuerySelect SelectAllChilds<TEntity, TChild>(Expression<Func<TEntity, object>> col, string alias = null)
		{
			var attribute = MLUtils.GetAttribute<TEntity>();
			var ns = attribute.NamespaceVarName;

			SelectColumns.Add($"{col.NodeNameWithAlias(alias, attribute)}/{ns}:{typeof(TChild).Name}");

			return this;
		}

		public XQuerySelect Copy()
		{
			return this.Clone();
		}
	}

	public class ChildMapping<TFrom, TTo>
	{
		public ChildMapping(Expression<Func<TFrom, object>> from, Expression<Func<TTo, object>> to)
		{
			From = from;
			To = to;
		}

		public Expression<Func<TFrom, object>> From { get; private set; }

		public Expression<Func<TTo, object>> To { get; private set; }
	}

}
