using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CommandLineArguments
{
	internal class CommandLineArgumentEnumerator : IEnumerator<KeyValuePair<string, object>>
	{
		private readonly IEnumerator<string> _argumentsEnumerator;
		private readonly IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> _aliasDictionary;
		private KeyValuePair<string, object>? _current;

		public CommandLineArgumentEnumerator(IEnumerator<string> argumentsEnumerator, IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> aliasDictionary)
		{
			if (argumentsEnumerator == null)
			{
				throw new ArgumentNullException("argumentsEnumerator");
			}

			if (aliasDictionary == null)
			{
				throw new ArgumentNullException("aliasDictionary");
			}

			_argumentsEnumerator = argumentsEnumerator;
			_aliasDictionary = aliasDictionary;
		}

		public bool MoveNext()
		{
			var result = false;

			if (_argumentsEnumerator.MoveNext())
			{
				var item = String.Format("{0}", _argumentsEnumerator.Current);

				var regex = new Regex(@"^(?:/|(?:--?))(.+)$");

				while ((regex.IsMatch(item) == false) && _argumentsEnumerator.MoveNext())
				{
					item = String.Format("{0}", _argumentsEnumerator.Current);
				}

				var match = regex.Match(item);

				if (match.Success)
				{
					var itemWithoutPrefix = match.Groups[1].Value;
					string key;
					object value;

					// TODO: check the attribute for Default and IsFlag

					if (itemWithoutPrefix.Contains("="))
					{
						var keyValuePair = itemWithoutPrefix.Split('=');
						key = keyValuePair[0];
						value = RemoveQuotes(keyValuePair[1]);

						_current = new KeyValuePair<string, object>(key, value);
						result = true;
					}
					else
					{
						key = itemWithoutPrefix;

						Tuple<PropertyInfo, CommandLineArgumentAttribute> tuple;
						if (_aliasDictionary.TryGetValue(key, out tuple))
						{
							if (tuple.Item2.IsFlag)
							{
								value = true;
							}
							else
							{
								if (_argumentsEnumerator.MoveNext() == false)
								{
									throw new ArgumentException(String.Format("Unexpected end of the argument token stream. Expected a value for argument '{0}'.", key));
								}

								value = RemoveQuotes(String.Format("{0}", _argumentsEnumerator.Current));
							}

							_current = new KeyValuePair<string, object>(key, value);
							result = true;
						}
					}
				}
			}

			return result;
		}

		private static string RemoveQuotes(string str)
		{
			var quotedString = Regex.Match(str, @"^([""'])([^\1]*)\1$");
			var result = str;

			if (quotedString.Success)
			{
				result = quotedString.Groups[2].Value;
			}

			return result;
		}

		public void Reset()
		{
			_current = null;
			_argumentsEnumerator.Reset();
		}

		public KeyValuePair<string, object> Current
		{
			get
			{
				if (_current.HasValue == false)
				{
					throw new InvalidOperationException();
				}

				return _current.Value;
			}
		}

		object IEnumerator.Current { get { return Current; } }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~CommandLineArgumentEnumerator()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources

				_argumentsEnumerator.Dispose();
			}

			// dispose native resources
		}
	}
}
