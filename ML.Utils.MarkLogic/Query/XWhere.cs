using ML.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.MarkLogic
{
    [Serializable]
    public class XWhere<TEntity> : XOrderBy<TEntity>
    {
        internal XWhere(string dbPath)
            : base(dbPath)
        {

        }

        public XWhere<TEntity> WhereEmpty()
        {
            return this;
        }

        public XWhere<TEntity> Where(Func<XFilterBuilder<TEntity>, XFilter<TEntity>> builder)
        {
            return Where<TEntity>(builder);
        }

        public XWhere<TEntity> Where<TOther>(Func<XFilterBuilder<TOther>, XFilter<TOther>> builder)
        {
            if (builder == null)
            {
                return this;
            }

            return Where(XExtensions.GetFilter(builder));
        }

        public XWhere<TEntity> Where(XFilter filter)
        {
            if (filter == null)
            {
                return this;
            }

            var condition = filter.Render();

            return Where(condition);
        }

        public XWhere<TEntity> Where(string condition)
        {
            return Where(new[] { condition });
        }

        public XWhere<TEntity> Where(IEnumerable<string> conditions)
        {
            QueryWheres.AddRange(conditions.Where(c => !string.IsNullOrEmpty(c)));

            return this;
        }

        public new XWhere<TEntity> Copy()
        {
            return this.Clone();
        }
    }
}
