using System;
using System.Linq.Expressions;

namespace ML.Utils.MarkLogic.DataContext.Impl
{
    internal partial class XEntityNodeImpl<T, TId> : XEntityNode<T, TId> where T : XEntity<TId>
    {
        readonly XDataContext _db;
        readonly XNodeFunc<T> _nodeFunc;
        //readonly Expression<Func<T, object>> _primaryNode = a => a.Id;
        readonly Expression<Func<T, TId>> _primaryNode = a => a.Id;

        internal XEntityNodeImpl(XDataContext db)
        {
            _db = db;
            _nodeFunc = XBuilder<T>.NodeFunc;
        }
    }
}
