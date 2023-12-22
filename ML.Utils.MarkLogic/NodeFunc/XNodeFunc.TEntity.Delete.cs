using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public partial class XNodeFunc<TEntity>
    {
        public string Delete(object id)
        {
            Initialize(false);

            _query.AppendFormat("if (count({0}) = 1) then xdmp:document-delete({1}) else ()"
                    , GetDocumentFile(id, true, false)
                    , GetDocumentFile(id, false));

            return _query.ToString();
        }

        public string DeleteField<TField>(Expression<Func<TEntity, TField>> fieldNode)
        {
            Initialize();

            _query.Append($"xdmp:node-delete({GetDocumentQuery()}/{fieldNode.NodeNameByGeneric()})");

            return _query.ToString();
        }

        public string DeleteElem<TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);
            var condition = XBuilder<TElem>.Filter.Eq(elemPrimaryNode, elemPrimaryValue, alias: string.Empty).Render();

            _query.AppendLine($"xdmp:node-delete({GetDocumentFile(primaryValue)}/{_attr.NamespaceVarName}:{listNodeName}/{_attr.NamespaceVarName}:{typeof(TElem).Name}[{condition}])");

            return _query.ToString();
        }

        public string DeleteElem<TElem>(Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode)
        {
            Initialize();

            var listNodeName = std.PropNameByGeneric(elemsNode);

            _query.AppendLine($"xdmp:node-delete({GetDocumentQuery()}/{_attr.NamespaceVarName}:{listNodeName}/{_attr.NamespaceVarName}:{typeof(TElem).Name})");

            return _query.ToString();
        }
    }
}
