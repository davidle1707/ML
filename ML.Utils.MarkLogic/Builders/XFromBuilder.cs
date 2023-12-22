using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public static class XFromBuilder<T>
    {
        public static XJoin<T> From(string dbPath, string alias = null)
        {
            var attrFrom = XUtils.Attribute<T>();

            return new XJoin<T>(dbPath)
            {
                FromInfo =
                {
                    Attribute = attrFrom,
                    Alias = XUtils.Alias<T>(alias, attrFrom),
                    DocumentQuery = XUtils.GetDocumentQuery<T>(attrFrom)
                }
            };
        }

        public static XJoin<TElem> FromElem<TId, TElem>(string dbPath, Expression<Func<T, TId>> primaryNode, TId primaryValue, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null)
        {
            var condition = XBuilder<T>.Filter.Eq(primaryNode, primaryValue, alias: string.Empty).Render();

            return ProcessFromElem(dbPath, elemsNode, alias, new[] { condition });
        }

        public static XJoin<TElem> FromElem<TId, TElem>(string dbPath, Expression<Func<T, TId>> primaryNode, IEnumerable<TId> primaryValues, Expression<Func<T, IList<TElem>>> elemsNode, string alias = null)
        {
            var condition = XBuilder<T>.Filter.In(primaryNode, primaryValues).Render();

            return ProcessFromElem(dbPath, elemsNode, alias, new[] { condition });
        }

        private static XJoin<TElem> ProcessFromElem<TElem>(string dbPath, Expression<Func<T, IList<TElem>>> elemsNode, string alias, IEnumerable<string> conditions)
        {
            var attrFrom = XUtils.Attribute<T>();
          
            return new XJoin<TElem>(dbPath)
            {
                FromInfo =
                {
                    Attribute = attrFrom,
                    Alias = XUtils.Alias<T>(alias, attrFrom),
                    DocumentQuery = XUtils.GetDocumentInListQuery<T, TElem>(elemsNode, attrFrom, conditions)
                }
            };
        }
    }
}
