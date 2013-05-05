using System;
using System.Linq;
using System.Reflection;

namespace CommandLineArguments
{
	public static class CommandLineArgumentConfigurator
	{
		public static T Configure<T>(params string [] commandLineArguments) where T : class, new()
		{
			var aliasDictionary = AliasDictionary.Create<T>();
			var configuration = new T();

			foreach (var argument in new CommandLineArgumentEnumerable(commandLineArguments, aliasDictionary))
			{
				Tuple<PropertyInfo, CommandLineArgumentAttribute> property;
				if (aliasDictionary.TryGetValue(argument.Key, out property))
				{
					var propertyInfo = property.Item1;
					var convertedValue = ConvertValue(propertyInfo.PropertyType, argument.Value);

					propertyInfo.SetValue(configuration, convertedValue, new object[0]);
				}
			}

			return configuration;
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
				var methodName = wasNullable
					? "ParseNullableEnum"
					: "ParseEnum";

				result = typeof (CommandLineArgumentConfigurator).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic)
					.MakeGenericMethod(type)
					.Invoke(null, new[] { result });
			}
			else if (type.GetInterfaces().Contains(typeof (IConvertible)))
			{
				result = Convert.ChangeType(value, type);
			}
			// TODO: is this needed? the important ValueTypes are IConvertible anyway...
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

			if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof (Nullable<>)))
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
	}
}
