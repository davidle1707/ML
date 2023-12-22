using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.MarkLogic.Query;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XQuery
    {
        #region Property

        internal _FromInfo FromInfo { get; } = new _FromInfo();

        internal List<_JoinInfo> JoinInfos { get; } = new List<_JoinInfo>();

        internal List<_OrderByInfo> OrderByInfos { get; } = new List<_OrderByInfo>();

        internal List<string> QueryWheres { get; } = new List<string>();

        internal XFuncCollection FuncCollecion { get; set; }

        internal XFuncAggregation FuncAggregation { get; set; }

        internal XPageOption Paging { get; set; } = new XPageOption();

        internal List<string> SelectColumns { get; } = new List<string>();

        #endregion

        #region Constructor

        protected readonly string DbPath;

        internal XQuery(string dbPath)
        {
            DbPath = dbPath;
        }

        #endregion

        #region Common

        protected void CheckCallFrom()
        {
            if (!FromInfo.IsValid)
            {
                throw new Exception("Call From<TFrom> first.");
            }
        }

        protected StringBuilder GetBaseQuery(bool useParam, string param = "$db")
        {
            if (!FromInfo.IsValid || SelectColumns.Count == 0)
            {
                throw new Exception("From or Select clause is empty");
            }

            var body = new StringBuilder();

            body.AppendLine(XUtils.DeclareFunction(DbPath, XUtils.FunctionRemoveNamespace));

            var attributes = new List<XEntityAttribute> { FromInfo.Attribute };

            foreach (var joinInfo in JoinInfos.Where(joinInfo => attributes.All(a => a.EntityName != joinInfo.Attribute.EntityName)))
            {
                attributes.Add(joinInfo.Attribute);
            }

            attributes.ForEach(a => body.AppendLine(XUtils.DeclareNamespace(a)));

            if (useParam)
            {
                body.AppendLine($"let {param}:= (");
            }

            body.AppendLine(FromInfo.ToQuery());

            if (JoinInfos.Count > 0)
            {
                body.AppendLine(string.Join(Environment.NewLine, JoinInfos.Select(j => j.ToQuery())));
            }

            if (QueryWheres.Any(w => !string.IsNullOrEmpty(w)))
            {
                body.AppendLine("where " + string.Join(" and ", QueryWheres.Where(w => !string.IsNullOrEmpty(w))));
            }

            if (OrderByInfos.Count > 0)
            {
                body.AppendLine("order by " + string.Join(", ", OrderByInfos.Select(o => o.ToQuery())));
            }

            body.AppendLine("return " + string.Join(", ", SelectColumns));

            if (useParam)
            {
                body.AppendLine(")");
            }

            return body;
        }

        #endregion
    }
}
