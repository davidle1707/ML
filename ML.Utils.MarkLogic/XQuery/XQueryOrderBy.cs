using System;
using System.Linq.Expressions;
using System.Reflection;
using ML.Common;

namespace ML.Utils.MarkLogic.XQuery
{
    [Serializable]
	public class XQueryOrderBy : XQuerySelect
	{
		public XQueryOrderBy(string dbPath)
			: base(dbPath)
	    {
		    
	    }

		/// <summary>
		/// 1 ascending
		/// </summary>
		public XQueryOrderBy OrderByDefault()
		{
			OrderByInfos.Add(new QueryOrderByInfo("1"));
			
			return this;
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryOrderBy OrderBy<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
            return OrderBy(col, alias, false);
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        public XQueryOrderBy OrderByDescending<TEntity>(Expression<Func<TEntity, object>> col, string alias = null)
		{
            return OrderBy(col, alias, true);
		}

        /// <summary>
        /// if alias is null, use entity name as alias
        /// </summary>
        private XQueryOrderBy OrderBy<TEntity>(Expression<Func<TEntity, object>> col, string alias, bool descending)
		{
            CheckValid();

			var propType = ((PropertyInfo)std.MemberInfo(col)).PropertyType;

			OrderByInfos.Add(new QueryOrderByInfo
				                 {
									 Column = col.NodeNameWithAlias(alias),
									 ColumnIsString = propType == typeof(string),
									 ColumnIsNullable = propType.IsNullableType(),
									 IsDescending = descending
				                 });

			return this;
		}
		
        public new XQueryOrderBy Copy()
        {
            return this.Clone();
        }
	}
}
