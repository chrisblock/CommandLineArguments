using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandLineArguments
{
	public static class CommandLineArgumentConfigurator
	{
		public static T Configure<T>(params string [] commandLineArguments) where T : class, new()
		{
			var resultType = typeof (T);
			var aliasDictionary = BuildAliasDictionary(resultType);
			var configuration = new T();

			var enumerator = commandLineArguments.GetEnumerator();
			while (enumerator.MoveNext())
			{
				// TODO: check the attribute for Default and IsFlag
				var keyValuePair = enumerator.GetNextKeyValuePair();

				if ((keyValuePair.Key != null) && (keyValuePair.Value != null))
				{
					Tuple<PropertyInfo, CommandLineArgumentAttribute> property;
					if (aliasDictionary.TryGetValue(keyValuePair.Key, out property))
					{
						var propertyInfo = property.Item1;
						var convertedValue = ConvertValue(propertyInfo.PropertyType, keyValuePair.Value);

						propertyInfo.SetValue(configuration, convertedValue, new object[0]);
					}
				}
			}

			return configuration;
		}

		private static IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> BuildAliasDictionary(IReflect type)
		{
			var result = new Dictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>>();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Select(x => new Tuple<PropertyInfo, CommandLineArgumentAttribute>(x, x.GetCustomAttributes(typeof(CommandLineArgumentAttribute), false).Cast<CommandLineArgumentAttribute>().SingleOrDefault()));

			foreach (var property in properties)
			{
				var propertyInfo = property.Item1;
				var attribute = property.Item2;

				foreach (var alias in attribute.Aliases)
				{
					if (result.ContainsKey(alias))
					{
						throw new ArgumentException(String.Format("Multiple properties cannot have the same command line alias:{0}\tAlias = '{1}'{0}\tProperty1 = '{2}'{0}\tProperty2 = '{3}'", Environment.NewLine, alias, result[alias], propertyInfo));
					}

					result[alias] = property;
				}
			}

			return result;
		}

		private static KeyValuePair<string, object> GetNextKeyValuePair(this IEnumerator enumerator)
		{
			var result = new KeyValuePair<string, object>();

			var item = String.Format("{0}", enumerator.Current);

			var matches = Regex.Match(item, @"^(?:/|(?:--?))(.+)$");

			if (matches.Success)
			{
				var itemWithoutPrefix = matches.Groups[1].Value;
				string key;
				object value;

				if (itemWithoutPrefix.Contains("="))
				{
					var keyValuePair = itemWithoutPrefix.Split('=');
					key = keyValuePair[0];
					value = RemoveQuotes(keyValuePair[1]);
				}
				else
				{
					enumerator.MoveNext();
					key = itemWithoutPrefix;
					value = RemoveQuotes(String.Format("{0}", enumerator.Current));
				}

				result = new KeyValuePair<string, object>(key, value);
			}

			return result;
		}

		private static object ConvertValue(Type t, object value)
		{
			Type type;
			var wasNullable = TryUnwrapNullable(t, out type);
			object result = String.Format("{0}", value);

			if (type == value.GetType())
			{
				result = value;
			}
			else if (type.IsEnum)
			{
				var methodName = wasNullable ? "ParseNullableEnum" : "ParseEnum";
				result = typeof(CommandLineArgumentConfigurator).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic)
					.MakeGenericMethod(type)
					.Invoke(null, new[] { result });
			}
			else if (type.GetInterfaces().Contains(typeof(IConvertible)))
			{
				result = Convert.ChangeType(value, type);
			}
			// TODO: is this needed? the important ValueTypes are IConvertable anyway...
			//else if (type.IsValueType)
			//{
			//    result = Convert.ChangeType(value, type);
			//}

			return result;
		}

		private static bool TryUnwrapNullable(Type type, out Type nonNullableType)
		{
			var result = false;
			nonNullableType = type;

			if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
			{
				result = true;
				nonNullableType = type.GetGenericArguments().Single();
			}

			return result;
		}

		private static TEnum? ParseNullableEnum<TEnum>(string value) where TEnum : struct 
		{
			TEnum? result = null;

			TEnum output;
			if (Enum.TryParse(value, true, out output))
			{
				result = output;
			}

			return result;
		}

		private static TEnum ParseEnum<TEnum>(string value) where TEnum : struct
		{
			var result = default(TEnum);

			TEnum output;
			if (Enum.TryParse(value, true, out output))
			{
				result = output;
			}

			return result;
		}

		private static string RemoveQuotes(string str)
		{
			var matchesDoubleQuotes = Regex.Match(str, @"^""([^""]+)""$");
			var matchesSingleQuotes = Regex.Match(str, @"^'([^']+)'$");
			var result = str;

			if (matchesDoubleQuotes.Success)
			{
				result = matchesDoubleQuotes.Groups[1].Value;
			}
			else if (matchesSingleQuotes.Success)
			{
				result = matchesSingleQuotes.Groups[1].Value;
			}

			return result;
		}
	}
}
