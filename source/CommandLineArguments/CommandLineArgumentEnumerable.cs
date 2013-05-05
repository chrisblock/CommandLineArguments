using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CommandLineArguments
{
	public class CommandLineArgumentEnumerable : IEnumerable<KeyValuePair<string, object>>
	{
		private readonly IEnumerable<string> _arguments;
		private readonly IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> _aliasDictionary;

		public CommandLineArgumentEnumerable(IEnumerable<string> arguments, IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> aliasDictionary)
		{
			_arguments = arguments;
			_aliasDictionary = aliasDictionary;
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return new CommandLineArgumentEnumerator(_arguments.GetEnumerator(), _aliasDictionary);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
