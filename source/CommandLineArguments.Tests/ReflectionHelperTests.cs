using System;
using System.Reflection;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace CommandLineArguments.Tests
{
	[TestFixture]
	public class ReflectionHelperTests
	{
		[Test]
		public void GetMethodInfo_SingleGenericParameter_Null_ThrowsException()
		{
			Assert.That(() => ReflectionHelper.GetMethodInfo<int>(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void GetMethodInfo_TwoGenericParameters_Null_ThrowsException()
		{
			Assert.That(() => ReflectionHelper.GetMethodInfo<int, bool>(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void GetMethodInfo_SingleGenericParameter_StringIsNullOrWhiteSpace_ReturnsStringIsNullOrWhiteSpaceMethodInfo()
		{
			var expected = typeof (String).GetMethod("IsNullOrWhiteSpace", BindingFlags.Static | BindingFlags.Public);
			var actual = ReflectionHelper.GetMethodInfo(() => String.IsNullOrWhiteSpace(null));

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void GetMethodInfo_TwoGenericParameters_StringContains_ReturnsEqualsContainsMethodInfo()
		{
			var expected = typeof (string).GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public);
			var actual = ReflectionHelper.GetMethodInfo((string s) => s.Contains(null));

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
