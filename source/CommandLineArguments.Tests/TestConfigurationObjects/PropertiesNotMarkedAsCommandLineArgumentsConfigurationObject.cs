namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class PropertiesNotMarkedAsCommandLineArgumentsConfigurationObject
	{
		[CommandLineArgument("a")]
		public string PropertyOne { get; set; }

		public string PropertyTwo { get; set; }
	}
}
