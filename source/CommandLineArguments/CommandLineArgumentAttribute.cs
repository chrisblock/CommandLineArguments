using System;

namespace CommandLineArguments
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class CommandLineArgumentAttribute : Attribute
	{
		public string[] Aliases { get; private set; }

		public CommandLineArgumentAttribute(params string[] aliases)
		{
			Aliases = aliases;
		}

		// TODO: implement the following?
		//public bool IsFlagArgument
		//{
		//    get
		//    {
		//        throw new NotImplementedException();
		//    }
		//    set
		//    {
		//        throw new NotImplementedException();
		//    }
		//}
	}
}
