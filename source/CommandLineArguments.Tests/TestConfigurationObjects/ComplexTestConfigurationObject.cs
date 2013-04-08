namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class ComplexTestConfigurationObject
	{
		[CommandLineArgument("p", "prop")]
		public int Property { get; set; }

		[CommandLineArgument("p2", "prop2")]
		public decimal? Property2 { get; set; }

		[CommandLineArgument("p3", "prop3")]
		public ConfigurationEnum? Property3 { get; set; }

		[CommandLineArgument("p4", "prop4")]
		public DefaultableEnum Property4 { get; set; }

		[CommandLineArgument("p5", "prop5", IsFlag = true)]
		public bool Property5 { get; set; }
	}
}
