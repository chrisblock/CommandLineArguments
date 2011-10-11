namespace EventUtilities.Tests
{
	public class ComplexTestConfigurationObject
	{
		[CommandLineArgument(Aliases = new[] { "p", "prop" })]
		public int Property { get; set; }

		[CommandLineArgument(Aliases = new[] { "p2", "prop2" })]
		public decimal? Property2 { get; set; }
	}
}
