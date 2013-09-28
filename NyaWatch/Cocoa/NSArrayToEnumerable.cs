using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	public static class NSArrayToEnumerable
	{
		public static IEnumerable<T> ToEnumerable<T>(this NSArray items) where T : NSObject
		{
			return NSArray.ArrayFromHandle<T> (items.Handle);
		}
	}
}

