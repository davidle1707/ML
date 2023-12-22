using ML.Utils.MarkLogic.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic
{
    public class XJoinBuilder
    {
        internal readonly List<_JoinInfo> Infos = new List<_JoinInfo>();

        internal XJoinBuilder()
        {
        }
    }

    public class XJoinBuilder<TFrom> : XJoinBuilder
    {
        internal XJoinBuilder()
        {
        }

        public XJoinBuilder<TFrom> Inner<TJoin>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TFrom, object>> colFrom, string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            var condition = XBuilder<TJoin>.Filter.EqField(colJoin, colFrom, alias: string.Empty, alias2: aliasFrom).Render();

            return Process<TJoin>(true, condition, aliasJoin);
        }

        public XJoinBuilder<TFrom> Inner<TJoin>(string condition, string alias = null)
        {
            return Process<TJoin>(true, condition, alias);
        }

        public XJoinBuilder<TFrom> Left<TJoin>(Expression<Func<TJoin, object>> colJoin, Expression<Func<TFrom, object>> colFrom, string aliasJoin = null, string aliasFrom = null, XFilter filter = null)
        {
            var condition = XBuilder<TJoin>.Filter.EqField(colJoin, colFrom, alias: string.Empty, alias2: aliasFrom).Render();

            return Process<TJoin>(false, condition, aliasJoin, filter); ;
        }
        
        public XJoinBuilder<TFrom> Left<TJoin>(string condition, string aliasJoin = null)
        {
            return Process<TJoin>(false, condition, aliasJoin);
        }

        private XJoinBuilder<TFrom> Process<TJoin>(bool inner, string condition, string aliasJoin = null, XFilter filter = null)
        {
            var attrJoin = XUtils.Attribute<TJoin>();

            if (filter != null)
            {
                condition += $" and ({filter.Render()})";
            }

            var info = new _JoinInfo(inner)
            {
                Attribute = attrJoin,
                Condition = condition,
                Alias = XUtils.Alias<TJoin>(aliasJoin, attrJoin),
                DocumentQuery = XUtils.GetDocumentQuery<TJoin>(attrJoin)
            };

            Infos.Add(info);

            return this;
        }
    }
}
