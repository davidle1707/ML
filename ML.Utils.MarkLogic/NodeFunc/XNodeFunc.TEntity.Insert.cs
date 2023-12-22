using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ML.Common;

namespace ML.Utils.MarkLogic
{
    public partial class XNodeFunc<TEntity>
    {
        public string Insert(TEntity source, Expression<Func<TEntity, object>> primaryNode)
        {
            return Insert<object>(source, primaryNode);
        }

        public string Insert<TId>(TEntity source, Expression<Func<TEntity, TId>> primaryNode)
        {
            Initialize(false);

            var primaryValue = primaryNode.Compile()(source);
            var entityElement = source.SerializeToXElement(_attr.NamespaceUrl);

            _query.AppendLine($"xdmp:document-insert({GetDocumentFile(primaryValue, false)},{entityElement})");

            return _query.ToString();
        }

        public string InsertField<TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            Initialize();

            var childElement = fieldValue.SerializeToXElement(_attr.NamespaceUrl);
            childElement.Name = _ns + std.PropNameByGeneric(fieldNode);

            _query.AppendLine($"xdmp:node-insert-child({GetDocumentFile(primaryValue)}, {childElement})");

            return _query.ToString();
        }

        public string InsertField<TField>(Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            Initialize();

            var childElement = fieldValue.SerializeToXElement(_attr.NamespaceUrl);
            childElement.Name = _ns + std.PropNameByGeneric(fieldNode);

            _query.AppendLine($"xdmp:node-insert-child({GetDocumentQuery()}, {childElement})");

            return _query.ToString();
        }

        public string InsertElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);

            var itemElement = elem.SerializeToXElement(_attr.NamespaceUrl);
            itemElement.Name = _ns + typeof(TElem).Name;

            _query.AppendLine($"xdmp:node-insert-child({GetDocumentFile(primaryValue)}/{_attr.NamespaceVarName}:{listNodeName}, {itemElement})");

            return _query.ToString();
        }

        public string InsertElem<TElem>(Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);

            var itemElement = elem.SerializeToXElement(_attr.NamespaceUrl);
            itemElement.Name = _ns + typeof(TElem).Name;

            _query.AppendLine($"xdmp:node-insert-child({GetDocumentQuery()}/{_attr.NamespaceVarName}:{listNodeName}, {itemElement})");

            return _query.ToString();
        }

        public string InsertMany(List<TEntity> sources, Expression<Func<TEntity, object>> primaryNode)
        {
            return InsertMany<object>(sources, primaryNode);
        }

        public string InsertMany<TId>(List<TEntity> sources, Expression<Func<TEntity, TId>> primaryNode)
        {
            if (sources.Count == 0)
            {
                return string.Empty;
            }

            var currentIgnoreDeclareNamespace = IgnoreDeclareNamespace;
            IgnoreDeclareNamespace = true;

            var queries = new List<string>(sources.Select(source => $"({Insert(source, primaryNode)})"));

            IgnoreDeclareNamespace = currentIgnoreDeclareNamespace;

            if (IgnoreDeclareNamespace)
            {
                return string.Join(", ", queries);
            }

            return XUtils.DeclareDefaultNamespace(_attr) + "\n" + XUtils.DeclareNamespace(_attr) + "\n" + string.Join(", ", queries);
        }
    }
}
