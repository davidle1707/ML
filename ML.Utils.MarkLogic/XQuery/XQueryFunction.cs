using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ML.Utils.MarkLogic.XQuery
{
	[Serializable]
	public enum XFuncCollection : short
	{
		None = 0,

		List = 1,

		First = 2,

		Elems = 3
	}

	[Serializable]
	public enum XFuncAggregation : short
	{
		None = 0,

		Count = 1,

		Sum = 2,

		Min = 3,

		Max = 4
	}

	[Serializable]
	public class XQueryFunction : XQuery
	{
        public XQueryFilter<T> Filter<T>() => new XQueryFilter<T>();

		public XQueryFunction(string dbPath)
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
		public string ToFirst(string xmlNodeName)
		{
			return QueryByFuncCollection(XFuncCollection.First, xmlNodeName);
		}

		public string ToList<TResult>()
		{
			return ToList(typeof(TResult).Name);
		}

		/// <summary>
		/// Make sure call Select function first
		/// </summary>
		public string ToList(string xmlNodeName)
		{
			return QueryByFuncCollection(XFuncCollection.List, xmlNodeName);
		}

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToChildList()
		{
			return QueryByFuncCollection(XFuncCollection.Elems, " ");
		}

        /// <summary>
        /// Make sure call Select function first
        /// </summary>
        public string ToList<TResult>(int pageSize, int pageNumber)
		{
			return ToList(typeof(TResult).Name, pageSize, pageNumber);
		}

		/// <summary>
		/// Make sure call Select function first
		/// </summary>
		public string ToList(string xmlNodeName, int pageSize, int pageNumber)
		{
			return QueryByFuncCollection(XFuncCollection.List, xmlNodeName, pageSize, pageNumber);
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
			CheckValid();

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
			CheckValid();

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
				items.Append(MLUtils.GetFunction(DbPath, MLUtils.FunctionRemoveNamespace, param));
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
