namespace CommandLineArguments.Tests.TestConfigurationObjects
{
	public class DuplicateAliasTestConfigurationObject
	{
		[CommandLineArgument("a")]
		public string PropertyOne { get; set; }

		[CommandLineArgument("a")]
		public string PropertyTwo { get; set; }
	}
}
