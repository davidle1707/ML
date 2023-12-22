using System;
using System.Linq.Expressions;

namespace ML.Common.Expressions
{
	/*https://github.com/jbevain/mono.linq.expressions/tree/master/Mono.Linq.Expressions*/
	public static class PredicateBuilder
	{
		//public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> source)
		//{
		//    return source;
		//}

		//public static Expression<Func<T, bool>> True<T>()
		//{
		//    return f => true;
		//}

		//public static Expression<Func<T, bool>> False<T>()
		//{
		//    return f => false;
		//}

		//public static Expression<Func<T, bool>> True<T>(this IEnumerable<T> source)
		//{
		//    return f => true;
		//}

		//public static Expression<Func<T, bool>> False<T>(this IEnumerable<T> source)
		//{
		//    return f => false;
		//}

		//public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		//{
		//    var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
		//    return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
		//}

		//public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		//{
		//    var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
		//    return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
		//}

		public static Expression<Func<T, bool>> True<T>()
		{
			return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), Expression.Parameter(typeof(T)));
		}

		public static Expression<Func<T, bool>> False<T>()
		{
			return Expression.Lambda<Func<T, bool>>(Expression.Constant(false), Expression.Parameter(typeof(T)));
		}

		public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
		{
			return self.Combine(expression, Expression.OrElse);
		}

		public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
		{
			return self.Combine(expression, Expression.AndAlso);
		}

		public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
		{
			return self.Combine(expression, Expression.And);
		}

		public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
		{
			return self.Combine(expression, Expression.Or);
		}

		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> self)
		{
			return self.Combine(Expression.Not);
		}
	}
}
