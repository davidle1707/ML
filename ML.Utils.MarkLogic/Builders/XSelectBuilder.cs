using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ML.Utils.MarkLogic
{
    public class XSelectBuilder
    {
        internal readonly List<string> Columns = new List<string>();

        internal XSelectBuilder()
        {
        }
    }

    public class XSelectBuilder<T> : XSelectBuilder
    {
        internal XSelectBuilder()
        {
        }

        public XSelectBuilder<T> FieldMap<TResult>(Expression<Func<T, object>> colEntity, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return Field(colEntity, aliasEntity, nodeName);
        }

        public XSelectBuilder<T> FieldMapFormat<TResult>(string formatString, List<Expression<Func<T, object>>> colEntities, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            formatString = formatString.Replace(" ", XSpecialCharacter.Space);

            var nodeName = std.PropName2(colResult); //only last property name
            var formats = colEntities.Select(colEntity => "{data(" + colEntity.NodeNameWithAlias(aliasEntity) + ")}").ToArray();

            Columns.Add("<" + nodeName + ">" + string.Format(formatString, formats) + "</" + nodeName + ">");

            return this;
        }

        public XSelectBuilder<T> Field(Expression<Func<T, object>> col, string alias = null, string nodeName = null)
        {
            var prop = (PropertyInfo)std.MemberInfo(col);

            if (prop.PropertyType.IsSimpleType())
            {
                Columns.Add(!string.IsNullOrEmpty(nodeName) ? "<" + nodeName + ">{data(" + col.NodeNameWithAlias(alias) + ")}</" + nodeName + ">" : col.NodeNameWithAlias(alias));
            }
            else
            {
                nodeName = !string.IsNullOrEmpty(nodeName) ? nodeName : prop.Name;

                var allKeyword = $"{XUtils.Attribute<T>().NamespaceVarName}:*";

                Columns.Add(string.Format("<{0}>{{{1}/{2}}}</{0}>", nodeName, col.NodeNameWithAlias(alias), allKeyword));
            }

            return this;
        }

        public XSelectBuilder<T> ChildMap<TMap, TResult>(Expression<Func<TResult, object>> colResult, List<XChildMap<T, TMap>> childColumns, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            var columns = (from childColumn in childColumns
                           let mappingNodeName = std.PropName2(childColumn.To)
                           select "<" + mappingNodeName + ">{data(" + childColumn.From.NodeNameWithAlias(aliasEntity) + ")}</" + mappingNodeName + ">"
                           ).ToList();

            Columns.Add("<" + nodeName + ">" + string.Join(string.Empty, columns) + "</" + nodeName + ">");

            return this;
        }

        public XSelectBuilder<T> FieldsMap<TResult>(string aliasEntity = null)
        {
            return Fields(aliasEntity, typeof(TResult).Name);
        }

        public XSelectBuilder<T> FieldsMap<TResult>(Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return Fields(aliasEntity, nodeName);
        }

        public XSelectBuilder<T> Fields(string alias = null, string nodeName = null)
        {
            var attr = XUtils.Attribute<T>();

            var allKeyword = $"{ attr.NamespaceVarName}:*";

            Columns.Add(!string.IsNullOrEmpty(nodeName)
                ? string.Format("<{0}>{{{1}/{2}}}</{0}>", nodeName, XUtils.Alias<T>(alias, attr), allKeyword)
                : XUtils.Alias<T>(alias, attr) + "/" + allKeyword);

            return this;
        }

        public XSelectBuilder<T> Field(string column, string nodeName)
        {
            Columns.Add("<" + nodeName + ">{" + column + "}</" + nodeName + ">");

            return this;
        }

        public XSelectBuilder<T> AggregateMap<TResult>(Expression<Func<T, object>> colEntity, XFuncAggregation function, Expression<Func<TResult, object>> colResult, string aliasEntity = null)
        {
            var nodeName = std.PropName2(colResult); //only last property name

            return Aggregate(colEntity, function, nodeName, aliasEntity);
        }

        public XSelectBuilder<T> Aggregate(Expression<Func<T, object>> col, XFuncAggregation function, string nodeName, string alias = null)
        {
            if (function != XFuncAggregation.None)
            {
                Columns.Add("<" + nodeName + ">{" + $"{function.ToString().ToLower()}({col.NodeNameWithAlias(alias)})" + "}</" + nodeName + ">");
            }

            return this;
        }

        public XSelectBuilder<T> Elems<TElem>(Expression<Func<T, IList<TElem>>> col, string alias = null)
        {
            var attr = XUtils.Attribute<T>();

            Columns.Add($"{col.NodeNameWithAlias(alias, attr)}/{attr.NamespaceVarName}:{typeof(TElem).Name}");

            return this;
        }

        public XSelectBuilder<T> Elem<TElem>(Expression<Func<T, IList<TElem>>> col, Expression<Func<T, TElem>> elem, string alias = null)
        {
            var attr = XUtils.Attribute<T>();

            Columns.Add($"{col.NodeNameWithAlias(alias, attr)}/{attr.NamespaceVarName}:{typeof(TElem).Name}/{elem.NodeNameWithAlias(alias, attr)}");

            return this;
        }
    }
}
