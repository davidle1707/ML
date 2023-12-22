using System.Reflection;
using System.Text;
using System.Xml.Linq;
using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public partial class XNodeFunc<TEntity> 
    {
        public string Update<TPrimary>(TEntity source, Expression<Func<TEntity, TPrimary>> primaryNode)
        {
            Initialize(false);

            var primaryValue = primaryNode.Compile()(source);

            var entityElement = source.SerializeToXElement(_attr.NamespaceUrl);
            _query.AppendLine($"xdmp:document-insert({GetDocumentFile(primaryValue, false)},{entityElement})");

            return _query.ToString();
        }

        public string UpdateMany<TPrimary>(List<TEntity> sources, Expression<Func<TEntity, TPrimary>> primaryNode)
        {
            if (sources.Count == 0)
            {
                return string.Empty;
            }

            var currentIgnoreDeclareNamespace = IgnoreDeclareNamespace;
            IgnoreDeclareNamespace = true;

            var queries = sources.Select(source => $"({Update(source, primaryNode)})").ToList();

            IgnoreDeclareNamespace = currentIgnoreDeclareNamespace;

            if (IgnoreDeclareNamespace)
            {
                return string.Join(", ", queries);
            }

            return XUtils.DeclareDefaultNamespace(_attr) + "\n" + XUtils.DeclareNamespace(_attr) + "\n" + string.Join(", ", queries);
        }

        public string UpdateField<TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            Initialize();

            var childElement = CreateElement(fieldNode, fieldValue);

            _query.AppendLine($"xdmp:node-replace({GetDocumentFile(primaryValue)}/{fieldNode.NodeNameByGeneric()},{childElement})");

            return _query.ToString();
        }

        public string UpdateField<TField>(Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            Initialize();

            var childElement = CreateElement(fieldNode, fieldValue);

            _query.AppendLine($"xdmp:node-replace({GetDocumentQuery()}/{fieldNode.NodeNameByGeneric()},{childElement})");

            return _query.ToString();
        }

        public string UpdateFields(object primaryValue, params XUpdateField<TEntity>[] childItems)
        {
            if (childItems == null || childItems.Length == 0)
            {
                return string.Empty;
            }

            var currentIgnoreDeclareNamespace = IgnoreDeclareNamespace;
            IgnoreDeclareNamespace = true;

            var queries = new List<string>();

            Initialize();

            var xdoc = GetDocumentFile(primaryValue);

            foreach (var child in childItems)
            {
                var childElement = CreateElement(child.Node, child.Value);

                if (childElement != null)
                {
                    var query = $"xdmp:node-replace({xdoc}/{child.Node.NodeNameByGeneric()},{childElement})";
                    queries.Add($"({query})");
                }
            }

            IgnoreDeclareNamespace = currentIgnoreDeclareNamespace;

            if (IgnoreDeclareNamespace)
            {
                return string.Join(", ", queries);
            }

            return XUtils.DeclareDefaultNamespace(_attr) + "\n" + XUtils.DeclareNamespace(_attr) + "\n" + string.Join(", ", queries);
        }

        public string UpdateElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);
            var condition = XBuilder<TElem>.Filter.Eq(elemPrimaryNode, elemPrimaryValue, alias: string.Empty).Render();

            var element = elem.SerializeToXElement(_attr.NamespaceUrl);
            element.Name = _ns + typeof(TElem).Name;

            _query.AppendLine($"xdmp:node-replace({GetDocumentFile(primaryValue)}/{_attr.NamespaceVarName}:{listNodeName}/{_attr.NamespaceVarName}:{element.Name.LocalName}[{condition}], {element})");

            return _query.ToString();
        }

        public string UpdateElems<TElem>(Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);

            var element = elem.SerializeToXElement(_attr.NamespaceUrl);
            element.Name = _ns + typeof(TElem).Name;

            _query.AppendLine($"xdmp:node-replace({GetDocumentQuery()}/{_attr.NamespaceVarName}:{listNodeName}/{_attr.NamespaceVarName}:{element.Name.LocalName}, {element})");

            return _query.ToString();
        }

        public string UpdateElemField<TElem, TElemField>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, Expression<Func<TElem, TElemField>> elemFieldNode, TElemField elemFieldValue)
        {
            Initialize();

            var elemsNodeName = std.PropNameByGeneric(elemsNode);
            var elemNodeName = typeof(TElem).Name;

            var elemCondition = XBuilder<TElem>.Filter.Eq(elemPrimaryNode, elemPrimaryValue, alias: string.Empty).Render();
            var elemField = CreateElement(elemFieldNode, elemFieldValue);

            _query.AppendLine($"xdmp:node-replace({GetDocumentFile(primaryValue)}/{_attr.NamespaceVarName}:{elemsNodeName}/{_attr.NamespaceVarName}:{elemNodeName}[{elemCondition}]/{elemFieldNode.NodeNameByGeneric()}, {elemField})");

            return _query.ToString();
        }

    }
}
