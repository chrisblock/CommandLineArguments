namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class TestConfigurationObject
	{
		[CommandLineArgument("p", "prop")]
		public string Property { get; set; }
	}
}
