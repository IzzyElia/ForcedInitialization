using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using System.Runtime.CompilerServices;

namespace Izzy.ForcedInitialization
{
	/// <summary>
	/// Calling ForceInitializer.Initialize() will force this class to run its static constructor if it hasn't already
	/// </summary>
	public interface IForceInitialize 
	{
	}
	public sealed class ForceInitializer
	{
		static bool initialized = false;
		public static void Initialize()
		{
			if (initialized)
			{
				throw new ForcedSingletonAlreadyInitializedException();
			}
			initialized = true;

			IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(IForceInitialize));
			foreach (Type type in types)
			{
				RuntimeHelpers.RunClassConstructor(type.TypeHandle);
			}
		}

		public class ForcedSingletonAlreadyInitializedException : Exception
		{
			public ForcedSingletonAlreadyInitializedException() : base($"ForcedSingleton.Initialize() called twice") { }
		}
	}
}