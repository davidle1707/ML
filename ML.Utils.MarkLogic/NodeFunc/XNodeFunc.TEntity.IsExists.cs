using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public partial class XNodeFunc<TEntity>
    {
        public string IsExists<TPrimary>(Expression<Func<TEntity, TPrimary>> primaryNode, TPrimary primaryValue)
        {
            Initialize();

            _query.AppendLine($"count({GetDocumentFile(primaryValue)}[1])");

            return _query.ToString();
        }

        public string IsFieldExists<TPrimary, TField>(TPrimary primaryValue, Expression<Func<TEntity, TField>> fieldNode)
        {
            Initialize();

            var fieldNodeName = fieldNode.NodeNameByGeneric();

            _query.AppendLine($"count({GetDocumentFile(primaryValue)}[empty({fieldNodeName}) = fn:false()][1])");

            return _query.ToString();
        }
    }
}
