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
    [Serializable]
    public sealed partial class XNodeFunc<TEntity> : XNodeFunc
    {
        internal bool IgnoreDeclareNamespace { get; set; }

        internal XNodeFunc()
        {
        }

        #region Init

        private StringBuilder _query;

        private XEntityAttribute _attr;

        private XNamespace _ns;
        private readonly XNamespace _nsXsd = "http://www.w3.org/2001/XMLSchema";
        private readonly XNamespace _nsXsi = "http://www.w3.org/2001/XMLSchema-instance";
        
        private void Initialize(bool declareNamespace = true)
        {
            _query = new StringBuilder();

            _attr = XUtils.Attribute<TEntity>();
            _ns = _attr.NamespaceUrl;

            if (declareNamespace && !IgnoreDeclareNamespace)
            {
                _query.AppendLine(XUtils.DeclareDefaultNamespace(_attr));
                _query.AppendLine(XUtils.DeclareNamespace(_attr));
            }
        }

        private string GetDocumentFile(object primaryValue, bool includeDocumentKeyword = true, bool includeDocumentQuery = true)
        {
            return XUtils.GetDocumentFile<TEntity>(primaryValue, _attr, includeDocumentKeyword, includeDocumentQuery);
        }

        private string GetDocumentQuery()
        {
            return XUtils.GetDocumentQuery<TEntity>(_attr);
        }

        #endregion
        
        #region Private

        /// <summary>
        /// only use after call LoadData function
        /// </summary>
        private XElement CreateElement<TEntity2, TField>(Expression<Func<TEntity2, TField>> node, object value)
        {
            var nodeType = ((PropertyInfo)std.MemberInfo(node)).PropertyType;

            if (value == null && !nodeType.IsNullableType())
            {
                return null;
            }

            if (value != null && !nodeType.IsSimpleType())
            {
                var objelement = value.SerializeToXElement(_attr.NamespaceUrl, value.GetType());
                objelement.Name = _ns + std.PropNameByGeneric(node);

                return objelement;
            }

            var element = new XElement(_ns + std.PropNameByGeneric(node));

            if (value == null)
            {
                element.Add(new XAttribute(XNamespace.Xmlns + "xsd", _nsXsd.ToString()));
                element.Add(new XAttribute(XNamespace.Xmlns + "xsi", _nsXsi.ToString()));
                element.Add(new XAttribute(_nsXsi + "nil", true));
            }
            else
            {
                if (nodeType == typeof(DateTime) || nodeType == typeof(DateTime?))
                {
                    element.Value = ((DateTime?)value).ToXQueryDateTime();
                }
                else if (nodeType == typeof(bool) || nodeType == typeof(bool?))
                {
                    element.Value = ((bool?)value).ToXQueryBool();
                }
                else
                {
                    element.Value = value.ToString();
                }
            }

            return element;
        }

        #endregion
    }
}
