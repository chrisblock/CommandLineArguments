namespace CommandLineArguments.Tests
{
	public class TestConfigurationObject
	{
		[CommandLineArgument(Aliases = new[] { "p", "prop" })]
		public string Property { get; set; }
	}
}
