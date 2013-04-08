using System;

namespace CommandLineArguments
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class CommandLineArgumentAttribute : Attribute
	{
		public string[] Aliases { get; private set; }
		public bool IsFlag { get; set; }

		public CommandLineArgumentAttribute(params string[] aliases)
		{
			Aliases = aliases;
		}
	}
}
