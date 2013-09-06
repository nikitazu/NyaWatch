using System;
using StructureMap;
using NyaWatch.Core.UI;
using NyaWatch.UI;

namespace NyaWatch
{
	/// <summary>
	/// Register IoC containers.
	/// </summary>
	public static class RegisterContainers
	{
		/// <summary>
		/// Register IoC containers.
		/// </summary>
		public static void Init()
		{
			ObjectFactory.Initialize ( x => {
				x.Scan( s => {
					s.AddAllTypesOf(typeof(Core.UI.IMessageBox));
					//s.AssemblyContainingType(typeof(RegisterContainers));
					s.TheCallingAssembly();
					//s.AssembliesFromApplicationBaseDirectory();
					s.WithDefaultConventions();
					s.LookForRegistries();
				});
			});

			Console.WriteLine (ObjectFactory.WhatDoIHave());
		}
	}
}

