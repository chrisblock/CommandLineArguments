namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class GetOnlyPropertyNotMarkedAsCommandLineArgumentsConfigurationObject
	{
		[CommandLineArgument("a")]
		public string PropertyOne { get; set; }

		public string PropertyTwo => PropertyOne;
	}
}
