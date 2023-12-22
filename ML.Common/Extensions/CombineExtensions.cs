using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ML.Common.Expressions
{
	/*https://github.com/jbevain/mono.linq.expressions/tree/master/Mono.Linq.Expressions*/
	public static class CombineExtensions
	{
		public static Expression<T> Combine<T>(this Expression<T> self, Func<Expression, Expression> combinator) where T : class
		{
			if (self == null)
				throw new ArgumentNullException("self");
			if (combinator == null)
				throw new ArgumentNullException("combinator");

			var parameters = ParametersFor(self);

			return Expression.Lambda<T>(combinator(RewriteBody(self, parameters)), parameters);
		}

		public static Expression<T> Combine<T>(this Expression<T> self, Expression<T> expression, Func<Expression, Expression, Expression> combinator) where T : class
		{
			if (self == null)
				throw new ArgumentNullException("self");
			if (expression == null)
				throw new ArgumentNullException("expression");
			if (combinator == null)
				throw new ArgumentNullException("combinator");

			var parameters = ParametersFor(self);

			return Expression.Lambda<T>(combinator(RewriteBody(self, parameters), RewriteBody(expression, parameters)), parameters);
		}

		static ParameterExpression[] ParametersFor(LambdaExpression lambda)
		{
			return lambda.Parameters.Select(p => Expression.Parameter(p.Type, p.Name)).ToArray();
		}

		static Expression RewriteBody(LambdaExpression expression, IEnumerable<ParameterExpression> parameters)
		{
			return new ParameterRewriter(expression.Parameters, parameters).Visit(expression.Body);
		}

		class ParameterRewriter : ExpressionVisitor
		{

			readonly IDictionary<ParameterExpression, ParameterExpression> parameterMapping;

			public ParameterRewriter(IEnumerable<ParameterExpression> candidates, IEnumerable<ParameterExpression> replacements)
			{
				parameterMapping = ParametersMappingFor(candidates, replacements);
			}

			static IDictionary<ParameterExpression, ParameterExpression> ParametersMappingFor(IEnumerable<ParameterExpression> candidates, IEnumerable<ParameterExpression> replacements)
			{
				return candidates.Zip(replacements, (candidate, replacement) => new { candidate, replacement }).ToDictionary(t => t.candidate, t => t.replacement);
			}

			protected override Expression VisitParameter(ParameterExpression expression)
			{
				ParameterExpression replacement;
				return parameterMapping.TryGetValue(expression, out replacement) ? replacement : expression;
			}
		}

	}
}
