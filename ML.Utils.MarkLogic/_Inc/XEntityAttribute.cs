using System;
using ML.Common;

namespace ML.Utils.MarkLogic
{
    [Serializable]
	public class XEntityAttribute : Attribute
	{
		public readonly string EntityName;

		public readonly string NamespaceVarName;

		public readonly string NamespaceUrl;

        public XEntityAttribute(string namespaceVarName)
            : this(string.Empty, namespaceVarName, string.Empty)
        {
        }

        public XEntityAttribute(string entityName, string namespaceVarName, string namespaceRootUrl)
		{
            EntityName = entityName;
            NamespaceUrl = namespaceRootUrl + (!namespaceRootUrl.EndsWith("/") ? "/" : string.Empty) + entityName;
            NamespaceVarName = namespaceVarName;
        }
     
	}
}
