namespace CommandLineArguments.Tests
{
	public class DuplicateAliasTestConfigurationObject
	{
		[CommandLineArgument(Aliases = new[] { "a" })]
		public string PropertyOne { get; set; }

		[CommandLineArgument(Aliases = new[] { "a" })]
		public string PropertyTwo { get; set; }
	}
}
