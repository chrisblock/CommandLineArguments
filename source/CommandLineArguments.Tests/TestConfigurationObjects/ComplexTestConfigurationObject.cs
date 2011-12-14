namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class ComplexTestConfigurationObject
	{
		[CommandLineArgument("p", "prop")]
		public int Property { get; set; }

		[CommandLineArgument("p2", "prop2")]
		public decimal? Property2 { get; set; }

		[CommandLineArgument("p3", "prop3")]
		public ConfigurationEnum? ConfigEnum { get; set; }
	}
}
