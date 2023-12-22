using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
    [Serializable]
    public class XQuery
    {
        #region Property

        internal QueryFromInfo FromInfo { get; } = new QueryFromInfo();

        internal List<QueryJoinInfo> JoinInfos { get; } = new List<QueryJoinInfo>();

        internal List<QueryOrderByInfo> OrderByInfos { get; } = new List<QueryOrderByInfo>();

        internal List<string> QueryWheres { get; } = new List<string>();

        internal XFuncCollection FuncCollecion { get; set; }

        internal XFuncAggregation FuncAggregation { get; set; }

        internal XPaging Paging { get; set; } = new XPaging();

        internal List<string> SelectColumns { get; } = new List<string>();

        #endregion

        #region Constructor

        protected readonly string DbPath;

        public XQuery(string dbPath)
        {
            DbPath = dbPath;
        }

        #endregion

        #region Common

        protected void CheckValid()
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

            body.AppendLine(MLUtils.DeclareFunction(DbPath, MLUtils.FunctionRemoveNamespace));

            var attributes = new List<XEntityAttribute> { FromInfo.Attribute };

            foreach (var joinInfo in JoinInfos.Where(joinInfo => attributes.All(a => a.EntityName != joinInfo.Attribute.EntityName)))
            {
                attributes.Add(joinInfo.Attribute);
            }

            attributes.ForEach(a => body.AppendLine(MLUtils.DeclareNamespace(a)));

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


    [Serializable]
    internal class QueryFromInfo
    {
        public string Alias { get; set; }

        public string Document { get; set; }

	    public string Condition { get; set; }

        public XEntityAttribute Attribute { get; set; }

        public bool IsValid => !string.IsNullOrWhiteSpace(Document);

        public string ToQuery()
        {
			var query = $"for {Alias} in {Document} ";

	        if (!string.IsNullOrWhiteSpace(Condition))
	        {
		        query = $"{query.TrimEnd()}[{Condition}]";
	        }

	        return query;
        }
    }

    [Serializable]
    internal class QueryJoinInfo
    {
        public QueryJoinInfo(bool innerJoin = true)
        {
            IsInnerJoin = innerJoin;
        }

        public bool IsInnerJoin { get; private set; }

        public string Alias { get; set; }

        public string Document { get; set; }

        public XEntityAttribute Attribute { get; set; }

        public string Condition { get; set; }

        public string ToQuery()
        {
            return IsInnerJoin
                       ? $"for {Alias} in {Document}[{Condition}] "
                : $"let {Alias} := {Document}[{Condition}] ";
        }
    }

    [Serializable]
    internal class QueryOrderByInfo
    {
	    public QueryOrderByInfo()
	    {
	    }

		public QueryOrderByInfo(string column) : this()
		{
			Column = column;
		}

        public string Column { get; set; }

        public bool ColumnIsString { get; set; }

		public bool ColumnIsNullable { get; set; }

        public bool IsDescending { get; set; }

        public string ToQuery()
        {
			if (ColumnIsString)
			{
				return $"fn:lower-case({Column}) {(IsDescending ? "descending" : "ascending")}";
			}

            return ColumnIsNullable
                ? string.Format("(if(not({0}/node())) then () else {0}) {1}", Column, (IsDescending ? "descending" : "ascending"))
                : $"{Column} {(IsDescending ? "descending" : "ascending")}";
        }
    }
}
