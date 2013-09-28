using System;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch.Threading
{
	/// <summary>
	/// Provide autorelease pool for NyaWatch.Core, in order to instantiate domain objects implementations, extending NSObject.
	/// </summary>
	public class AutoreleasePool : NSAutoreleasePool, Core.Threading.IAutoreleasePool
	{
		// empty
	}
}
