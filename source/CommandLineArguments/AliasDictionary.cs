using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLineArguments
{
	internal class AliasDictionary : IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>>
	{
		private readonly IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> _dictionary;

		public static AliasDictionary Create(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}

			var result = new Dictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>>();
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(x => Attribute.IsDefined(x, typeof (CommandLineArgumentAttribute)))
				.Select(x => new Tuple<PropertyInfo, CommandLineArgumentAttribute>(x, x.GetCustomAttributes<CommandLineArgumentAttribute>().SingleOrDefault()));

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

			return new AliasDictionary(result);
		}

		public static AliasDictionary Create<T>()
		{
			return Create(typeof (T));
		}

		public int Count { get { return _dictionary.Count; } }

		public bool IsReadOnly { get { return _dictionary.IsReadOnly; } }

		public ICollection<string> Keys { get { return _dictionary.Keys; } }

		public ICollection<Tuple<PropertyInfo, CommandLineArgumentAttribute>> Values { get { return _dictionary.Values; } }

		private AliasDictionary(IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> dictionary)
		{
			_dictionary = dictionary;
		}

		public IEnumerator<KeyValuePair<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>>> GetEnumerator()
		{
			return _dictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(KeyValuePair<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> item)
		{
			_dictionary.Add(item);
		}

		public void Clear()
		{
			_dictionary.Clear();
		}

		public bool Contains(KeyValuePair<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> item)
		{
			return _dictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>>[] array, int arrayIndex)
		{
			_dictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> item)
		{
			return _dictionary.Remove(item);
		}

		public bool ContainsKey(string key)
		{
			return _dictionary.ContainsKey(key);
		}

		public void Add(string key, Tuple<PropertyInfo, CommandLineArgumentAttribute> value)
		{
			_dictionary.Add(key, value);
		}

		public bool Remove(string key)
		{
			return _dictionary.Remove(key);
		}

		public bool TryGetValue(string key, out Tuple<PropertyInfo, CommandLineArgumentAttribute> value)
		{
			return _dictionary.TryGetValue(key, out value);
		}

		public Tuple<PropertyInfo, CommandLineArgumentAttribute> this[string key]
		{
			get { return _dictionary[key]; }
			set { _dictionary[key] = value; }
		}
	}
}
