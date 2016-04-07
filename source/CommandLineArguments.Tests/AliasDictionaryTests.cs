using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CommandLineArguments.Tests.TestConfigurationObjects;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

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
		public void Create_Null_ThrowsArgumentNullException()
		{
			Assert.That(() => AliasDictionary.Create(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Create_ObjectWithDuplicateAliases_ThrowsArgumentException()
		{
			Assert.That(() => AliasDictionary.Create(typeof (DuplicateAliasTestConfigurationObject)), Throws.ArgumentException);
		}

		[Test]
		public void Create_ObjectWithPropertiesThatAreNotMarked_DoesNotThrow()
		{
			Assert.That(() => AliasDictionary.Create(typeof (PropertiesNotMarkedAsCommandLineArgumentsConfigurationObject)), Throws.Nothing);
		}

		[Test]
		public void Create_ObjectWithGetOnlyPropertiesThatAreNotMarked_DoesNotThrow()
		{
			Assert.That(() => AliasDictionary.Create(typeof (GetOnlyPropertyNotMarkedAsCommandLineArgumentsConfigurationObject)), Throws.Nothing);
		}

		[Test]
		public void Count_IsEqualTo_TotalNumberOfAliasesOnPropertiesOnType()
		{
			var expected = typeof(ComplexTestConfigurationObject).GetProperties()
				.SelectMany(x => x.GetCustomAttributes<CommandLineArgumentAttribute>())
				.SelectMany(x => x.Aliases)
				.Count();

			Assert.That(_dictionary, Has.Count.EqualTo(expected));
		}

		[Test]
		public void IsReadOnly_ReturnsFalse()
		{
			Assert.That(_dictionary.IsReadOnly, Is.False);
		}

		[Test]
		public void Keys_ReturnsCollectionOfAliases()
		{
			var expected = typeof(ComplexTestConfigurationObject).GetProperties()
				.SelectMany(x => x.GetCustomAttributes<CommandLineArgumentAttribute>())
				.SelectMany(x => x.Aliases)
				.OrderBy(x => x);

			var keys = _dictionary.Keys.OrderBy(x => x);

			Assert.That(keys, Is.EquivalentTo(expected));
		}

		[Test]
		public void Values_ReturnsCollectionOfValues()
		{
			var expected = typeof(ComplexTestConfigurationObject).GetProperties()
				.SelectMany(x => x.GetCustomAttributes<CommandLineArgumentAttribute>(), (property, attribute) => new { Property = property, Attribute = attribute })
				.SelectMany(x => x.Attribute.Aliases, (x, alias) => new Tuple<PropertyInfo, CommandLineArgumentAttribute>(x.Property, x.Attribute))
				.OrderBy(x => x.Item1.Name);

			var keys = _dictionary.Values.OrderBy(x => x.Item1.Name);

			Assert.That(keys, Is.EquivalentTo(expected));
		}

		[Test]
		public void GetEnumerator_ReturnsEnumerator_HasCorrectNumberOfRecords()
		{
			var enumerator = _dictionary.GetEnumerator();

			var count = 0;

			while (enumerator.MoveNext())
			{
				count++;
			}

			Assert.That(count, Is.EqualTo(_dictionary.Count));
		}
	}
}
