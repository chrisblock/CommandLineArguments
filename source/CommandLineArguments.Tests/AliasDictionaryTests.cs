// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CommandLineArguments.Tests.TestConfigurationObjects;

using NUnit.Framework;

namespace CommandLineArguments.Tests
{
	[TestFixture]
	public class AliasDictionaryTests
	{
		private IDictionary<string, Tuple<PropertyInfo, CommandLineArgumentAttribute>> _dictionary;

		[SetUp]
		public void TestSetUp()
		{
			_dictionary = AliasDictionary.Create<ComplexTestConfigurationObject>();
		}

		[Test]
		public void Count_IsEqualToNumberOfPropertiesOnType()
		{
			var expected = typeof (ComplexTestConfigurationObject).GetProperties()
				.SelectMany(x => x.GetAttributesOfType<CommandLineArgumentAttribute>())
				.SelectMany(x => x.Aliases)
				.ToList();

			Assert.That(_dictionary.Count, Is.EqualTo(expected.Count));
		}
	}
}
