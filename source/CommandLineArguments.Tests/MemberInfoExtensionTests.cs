using System.Linq;

using NUnit.Framework;

namespace CommandLineArguments.Tests
{
	[TestFixture]
	public class MemberInfoExtensionTests
	{
		[Test]
		public void GetAttributesOfType_TestFixtureAttribute_ReturnsSingleAttribute()
		{
			var attributes = GetType().GetAttributesOfType<TestFixtureAttribute>(false).ToList();

			Assert.That(attributes, Is.Not.Empty);
			Assert.That(attributes.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetAttributesOfType_CommandLineArgumentAttribute_ReturnsEmpty()
		{
			var attributes = GetType().GetAttributesOfType<CommandLineArgumentAttribute>(false).ToList();

			Assert.That(attributes, Is.Empty);
		}
	}
}
