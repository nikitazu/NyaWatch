using System;

namespace NyaWatch.Core.Threading
{
	/// <summary>
	/// Provide autorelease pool for NyaWatch.Core, in order to instantiate domain objects implementations, extending NSObject.
	/// </summary>
	public interface IAutoreleasePool : IDisposable
	{
		// empty
	}
}

