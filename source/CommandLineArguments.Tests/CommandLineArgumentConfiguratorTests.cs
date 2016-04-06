using System;

using CommandLineArguments.Tests.TestConfigurationObjects;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace CommandLineArguments.Tests
{
	[TestFixture]
	public class CommandLineArgumentConfiguratorTests
	{
		[Test]
		public void Configure_EmptyStringArray_EmptyObject()
		{
			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>();

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.Null);
		}

		[Test]
		public void Configure_ArgumentNotMappedToPropertyAndPropertyWithDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "hello", "-p", value };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyExpectingAValueButNoValueProvided_ThrowsException()
		{
			var args = new[] { "-p" };

			Assert.That(() => CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args), Throws.ArgumentException);
		}

		[Test]
		public void Configure_PropertyWithDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] {"-p", value};

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithDoubleDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "--p", value };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_WithForwardSlashPrefixProperty_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "/p", value };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithSingleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("-p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithDoubleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("--p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithForwardSlashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("/p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedEmptyString_ConfigurationObjectWithSetProperty()
		{
			var value = String.Empty;
			var args = new[] { "-p", String.Format("'{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "-p", String.Format("'{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithDoubleDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "--p", String.Format("'{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedWithForwardSlashPrefixProperty_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "/p", String.Format("'{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithSingleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("-p='{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithDoubleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("--p='{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithForwardSlashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("/p='{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedEmptyString_ConfigurationObjectWithSetProperty()
		{
			var value = String.Empty;
			var args = new[] { "-p", String.Format("\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedPropertyWithDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "-p", String.Format("\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedPropertyWithDoubleDashPrefix_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "--p", String.Format("\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedWithForwardSlashPrefixProperty_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { "/p", String.Format("\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedPropertyWithSingleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("-p=\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedPropertyWithDoubleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("--p=\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_DoubleQuotedPropertyWithForwardSlashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { String.Format("/p=\"{0}\"", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithDoubleQuotesInTheValue_ConfigurationObjectWithSetProperty()
		{
			const string value = "value of \"hi\"";
			var args = new[] { String.Format("/p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithSingleQuotesInTheValue_ConfigurationObjectWithSetProperty()
		{
			const string value = "value of 'hi'";
			var args = new[] { String.Format("/p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_ComplexeProperties_ConfigurationObjectWithSetIntegerAndDecimalProperty()
		{
			const int value = 42;
			const decimal value2 = 4.35m;
			var args = new[] { String.Format("/p=\"{0}\"", value), "--p2", String.Format("{0}", value2) };

			var config = CommandLineArgumentConfigurator.Configure<ComplexTestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
			Assert.That(config.Property2, Is.EqualTo(value2));
		}

		[Test]
		public void Configure_ComplexeProperties_ConfigurationObjectWithSetIntegerDecimalAndEnumProperty()
		{
			const int value = 42;
			const decimal value2 = 4.35m;
			const ConfigurationEnum value3 = ConfigurationEnum.OptionOne;

			var args = new[] { String.Format("/p=\"{0}\"", value), "--p2", String.Format("{0}", value2) , "-p3", String.Format("{0}", value3)};

			var config = CommandLineArgumentConfigurator.Configure<ComplexTestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
			Assert.That(config.Property2, Is.EqualTo(value2));
			Assert.That(config.Property3, Is.EqualTo(value3));
			Assert.That(config.Property4, Is.EqualTo(DefaultableEnum.Default));
		}

		[Test]
		public void Configure_ComplexeProperties_ConfigurationObjectWithSetIntegerDecimalAndDefaultableEnumProperty()
		{
			const int value = 42;
			const decimal value2 = 4.35m;
			const DefaultableEnum value3 = DefaultableEnum.Property;

			var args = new[] { String.Format("/p=\"{0}\"", value), "--p2", String.Format("{0}", value2), "-p4", String.Format("{0}", value3) };

			var config = CommandLineArgumentConfigurator.Configure<ComplexTestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
			Assert.That(config.Property2, Is.EqualTo(value2));
			Assert.That(config.Property3, Is.Null);
			Assert.That(config.Property4, Is.EqualTo(value3));
		}

		[Test]
		public void Configure_ComplexeProperties_ConfigurationObjectWithSetIntegerDecimalAndEnumAndWithFlagArgumentPresentProperty()
		{
			const int value = 42;
			const decimal value2 = 4.35m;
			const ConfigurationEnum value3 = ConfigurationEnum.OptionOne;

			var args = new[] { "-p5", String.Format("/p=\"{0}\"", value), "--p2", String.Format("{0}", value2), "-p3", String.Format("{0}", value3) };

			var config = CommandLineArgumentConfigurator.Configure<ComplexTestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
			Assert.That(config.Property2, Is.EqualTo(value2));
			Assert.That(config.Property3, Is.EqualTo(value3));
			Assert.That(config.Property4, Is.EqualTo(DefaultableEnum.Default));
			Assert.That(config.Property5, Is.True);
		}

		[Test]
		public void Configure_PropertyWithForwardSlashDashPrefix_NotRecognized()
		{
			const string value = "value";
			var args = new[] { "/-p", value };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.Null);
		}

		[Test]
		public void Configure_DuplicateAliases_ThrowsException()
		{
			const string value = "value";
			var args = new[] { "-a", value, "/a", value };

			Assert.That(() => CommandLineArgumentConfigurator.Configure<DuplicateAliasTestConfigurationObject>(args), Throws.ArgumentException);
		}
	}
}
