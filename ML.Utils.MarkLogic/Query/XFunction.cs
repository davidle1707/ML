using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ML.Utils.MarkLogic
{
	[Serializable]
	public class XFunction<TEntity> : XFunction
    {
		internal XFunction(string dbPath)
			: base(dbPath)
		{

		}

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToFirst()
        {
            return base.ToFirst<TEntity>();
        }

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToList(XPageOption pageOption = null)
        {
            return base.ToList<TEntity>(pageOption);
        }

        public string Sum(Expression<Func<TEntity, object>> col, string alias = null)
		{
			return base.Sum<TEntity>(col, alias);
		}

		public string Min(Expression<Func<TEntity, object>> col, string alias = null)
		{
            return base.Min<TEntity>(col, alias);
        }

		public string Max(Expression<Func<TEntity, object>> col, string alias = null)
		{
            return base.Max<TEntity>(col, alias);
        }
		
	}

    [Serializable]
    public class XFunction : XQuery
    {
        internal XFunction(string dbPath)
            : base(dbPath)
        {
        }

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToFirst<TResult>()
        {
            return ToFirst(typeof(TResult).Name);
        }

        /// <summary>
		/// Make sure call Select function first
		/// </summary>
		public string ToFirst(string nodeName)
        {
            return QueryByFuncCollection(XFuncCollection.First, nodeName);
        }

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToList<TResult>(XPageOption pageOption = null)
        {
           return ToList(typeof(TResult).Name, pageOption);
        }

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToList(string nodeName, XPageOption pageOption = null)
        {
            if (pageOption != null && pageOption.PageSize != int.MaxValue)
            {
                return QueryByFuncCollection(XFuncCollection.List, nodeName, pageOption.PageSize, pageOption.PageNumber);
            }

            return QueryByFuncCollection(XFuncCollection.List, nodeName);
        }
       
        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToListElems()
        {
            return QueryByFuncCollection(XFuncCollection.Elems, " ");
        }

        public string Count()
        {
            SelectColumns.Clear();
            SelectColumns.Add("1");

            return QueryByFuncAggregation(XFuncAggregation.Count);
        }

        public string Sum<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
        {
            SelectColumns.Clear();
            SelectColumns.Add(col.NodeNameWithAlias(alias));

            return QueryByFuncAggregation(XFuncAggregation.Sum);
        }

        public string Min<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
        {
            SelectColumns.Clear();
            SelectColumns.Add(col.NodeNameWithAlias(alias));

            return QueryByFuncAggregation(XFuncAggregation.Min);
        }

        public string Max<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
        {
            SelectColumns.Clear();
            SelectColumns.Add(col.NodeNameWithAlias(alias));

            return QueryByFuncAggregation(XFuncAggregation.Max);
        }

        #region Private Processing

        private string QueryByFuncCollection(XFuncCollection function, string xmlNodeName, int? pageSize = null, int? pageNumber = null)
        {
            CheckCallFrom();

            if (SelectColumns.Count == 0)
            {
                throw new Exception("QuerySelect is empty");
            }

            if (string.IsNullOrEmpty(xmlNodeName))
            {
                throw new Exception("XmlNodeName is empty");
            }

            if (function != XFuncCollection.Elems)
            {
                var select = string.Format("<{0}>{{{1}}}</{0}>", xmlNodeName, string.Join(", ", SelectColumns));

                SelectColumns.Clear();
                SelectColumns.Add(select);
            }

            if (pageSize != null && pageNumber != null)
            {
                Paging.PageSize = pageSize.Value;
                Paging.PageNumber = pageNumber.Value;
            }
            else
            {
                Paging.NoPaging();
            }

            FuncCollecion = function;
            FuncAggregation = XFuncAggregation.None;

            return ToQuery();
        }

        private string QueryByFuncAggregation(XFuncAggregation function)
        {
            CheckCallFrom();

            FuncAggregation = function;
            FuncCollecion = XFuncCollection.None;

            return ToQuery();
        }

        private string ToQuery()
        {
            var param = "$db";

            var builder = GetBaseQuery(true, param);
            builder.AppendLine("return");
            builder.AppendLine("<Response xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");

            if (FuncAggregation != XFuncAggregation.None)
            {
                builder.AppendLine("<Value>{" + FuncAggregation.ToString().ToLower() + "(" + param + ")}</Value>");
            }
            else if (FuncCollecion != XFuncCollection.None)
            {
                if (Paging.IsValid)
                {
                    builder.AppendLine("<Total>{count(" + param + ")}</Total>");
                }

                if (FuncCollecion == XFuncCollection.First)
                {
                    param += "[1]";
                }
                else if (Paging.IsValid)
                {
                    param += $"[{Paging.StartIndex} to {Paging.EndIndex}]";
                }

                var items = new StringBuilder("<Items>{");
                items.Append(XUtils.GetFunction(DbPath, XUtils.FunctionRemoveNamespace, param));
                items.Append("}</Items>");
                builder.AppendLine(items.ToString());
            }

            //end builder
            builder.Append("</Response>");

            return builder.ToString();
        }

        #endregion
    }
}
