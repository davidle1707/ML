using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ML.Common;

namespace ML.Utils.MarkLogic
{
    /// <summary>
    /// Everythings are related to QueryHelper to render query, MAKE SURE USE Helper property
    /// </summary>
    [Serializable]
    public class XQueryMultipleBuilder
    {
        private readonly List<string> _namespaces;

        private readonly List<string> _queries;

        private readonly bool _ignoreDeclareNamespace;

        internal XQueryMultipleBuilder(bool ignoreDeclareNamespace = true)
        {
            _namespaces = new List<string>();

            _queries = new List<string>();

            _ignoreDeclareNamespace = ignoreDeclareNamespace;

        }

        public XQueryMultipleBuilder Add(string query)
        {
            _queries.Add($"({query})");

            return this;
        }

        public XQueryMultipleBuilder AddRange(IEnumerable<string> queries)
        {
            _queries.AddRange(queries.Select(q => $"({q})"));

            return this;
        }

        public XQueryMultipleBuilder AddAsFirst(string query)
        {
            _queries.Insert(0, $"({query})");

            return this;
        }

        public XQueryMultipleBuilder AddNamespace<T>(bool useAsDefault = false)
        {
            _namespaces.Add(XUtils.DeclareNamespace<T>(useAsDefault));

            return this;
        }

        public XQueryMultipleBuilder AddDefaultNamespace<T>()
        {
            _namespaces.Add(XUtils.DeclareDefaultNamespace<T>());

            return this;
        }

        public string ToQuery()
        {
            return string.Join("\n", _namespaces) + "\n" + string.Join(", ", _queries);
        }

        public bool HasQuery => _queries.Count > 0;

        #region Helper Functions

        public XQueryMultipleBuilder IsExists<TEntity, TPrimary>(Expression<Func<TEntity, TPrimary>> primaryNode, TPrimary primaryValue)
        {
            return Add(NodeFunc<TEntity>().IsExists(primaryNode, primaryValue));
        }

        public XQueryMultipleBuilder IsFieldExists<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode)
        {
            return Add(NodeFunc<TEntity>().IsFieldExists(primaryValue, fieldNode));
        }

        public XQueryMultipleBuilder Delete<TEntity>(object id)
        {
            return Add(NodeFunc<TEntity>().Delete(id));
        }

        public XQueryMultipleBuilder DeleteElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue)
        {
            return Add(NodeFunc<TEntity>().DeleteElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue));
        }

        public XQueryMultipleBuilder Insert<TEntity>(TEntity source, Expression<Func<TEntity, object>> primaryNode)
        {
            return Add(NodeFunc<TEntity>().Insert(source, primaryNode));
        }

        public XQueryMultipleBuilder InsertMany<TEntity>(List<TEntity> sources, Expression<Func<TEntity, object>> primaryNode)
        {
            return Add(NodeFunc<TEntity>().InsertMany(sources, primaryNode));
        }

        public XQueryMultipleBuilder InsertField<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return Add(NodeFunc<TEntity>().InsertField(primaryValue, fieldNode, fieldValue));
        }

        public XQueryMultipleBuilder InsertElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, TElem elem)
        {
            return Add(NodeFunc<TEntity>().InsertElem(primaryValue, elemsNode, elem));
        }

        public XQueryMultipleBuilder Update<TEntity, TPrimary>(TEntity source, Expression<Func<TEntity, TPrimary>> primaryNode)
        {
            return Add(NodeFunc<TEntity>().Update(source, primaryNode));
        }

        public XQueryMultipleBuilder UpdateMany<TEntity, TPrimary>(List<TEntity> sources, Expression<Func<TEntity, TPrimary>> primaryNode)
        {
            return Add(NodeFunc<TEntity>().UpdateMany(sources, primaryNode));
        }

        public XQueryMultipleBuilder UpdateField<TEntity, TField>(object primaryValue, Expression<Func<TEntity, TField>> fieldNode, TField fieldValue)
        {
            return Add(NodeFunc<TEntity>().UpdateField(primaryValue, fieldNode, fieldValue));
        }

        public XQueryMultipleBuilder UpdateField<TEntity>(object primaryValue, XUpdateFieldBuilder<TEntity> builder)
        {
            var fields = builder.Fields;

            if (fields.Length == 0)
            {
                return this;
            }

            return Add(NodeFunc<TEntity>().UpdateFields(primaryValue, fields));
        }

        public XQueryMultipleBuilder UpdateElem<TEntity, TElem>(object primaryValue, Expression<Func<TEntity, IList<TElem>>> elemsNode, Expression<Func<TElem, object>> elemPrimaryNode, object elemPrimaryValue, TElem elem)
        {
            return Add(NodeFunc<TEntity>().UpdateElem(primaryValue, elemsNode, elemPrimaryNode, elemPrimaryValue, elem));
        }

        public XQueryMultipleBuilder Save<TEntity>(bool isNew, TEntity source, Expression<Func<TEntity, object>> primaryNode)
        {
            return Add(isNew ? NodeFunc<TEntity>().Insert(source, primaryNode) : NodeFunc<TEntity>().Update(source, primaryNode));
        }

        #endregion

        public XNodeFunc<TEntity> NodeFunc<TEntity>() => new XNodeFunc<TEntity> { IgnoreDeclareNamespace = _ignoreDeclareNamespace };
    }
}
