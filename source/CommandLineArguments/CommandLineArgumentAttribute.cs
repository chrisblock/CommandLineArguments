using System;

namespace CommandLineArguments
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class CommandLineArgumentAttribute : Attribute
	{
		public string[] Aliases { get; set; }

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
