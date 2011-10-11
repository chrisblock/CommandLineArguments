﻿using System;

using NUnit.Framework;

namespace EventUtilities.Tests
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
			var args = new[] { string.Format("-p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithDoubleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { string.Format("--p={0}", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_PropertyWithForwardSlashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { string.Format("/p={0}", value) };

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
			var args = new[] { string.Format("-p='{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithDoubleDashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { string.Format("--p='{0}'", value) };

			var config = CommandLineArgumentConfigurator.Configure<TestConfigurationObject>(args);

			Assert.That(config, Is.Not.Null);
			Assert.That(config.Property, Is.EqualTo(value));
		}

		[Test]
		public void Configure_SingleQuotedPropertyWithForwardSlashAndEqualsSign_ConfigurationObjectWithSetProperty()
		{
			const string value = "value";
			var args = new[] { string.Format("/p='{0}'", value) };

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
			var args = new[] { string.Format("--p=\"{0}\"", value) };

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
