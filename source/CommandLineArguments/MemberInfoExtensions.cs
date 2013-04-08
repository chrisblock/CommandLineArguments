using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLineArguments
{
	public static class MemberInfoExtensions
	{
		public static IEnumerable<T> GetAttributesOfType<T>(this MemberInfo propertyInfo, bool inherit) where T : Attribute
		{
			return propertyInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>();
		}
	}
}
