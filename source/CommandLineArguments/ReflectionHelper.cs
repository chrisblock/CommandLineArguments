using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandLineArguments
{
	internal static class ReflectionHelper
	{
		public static MethodInfo GetMethodInfo<TResult>(Expression<Func<TResult>> lambda)
		{
			if (lambda == null)
			{
				throw new ArgumentNullException("lambda");
			}

			var methodCallExpression = lambda.Body as MethodCallExpression;

			return GetMethodInfo(methodCallExpression);
		}

		public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> lambda)
		{
			if (lambda == null)
			{
				throw new ArgumentNullException("lambda");
			}

			var methodCallExpression = lambda.Body as MethodCallExpression;

			return GetMethodInfo(methodCallExpression);
		}

		private static MethodInfo GetMethodInfo(MethodCallExpression methodCallExpression)
		{
			if (methodCallExpression == null)
			{
				throw new ArgumentNullException("methodCallExpression");
			}

			var method = methodCallExpression.Method;

			var result = method.IsGenericMethod
				? method.GetGenericMethodDefinition()
				: method;

			return result;
		}
	}
}
