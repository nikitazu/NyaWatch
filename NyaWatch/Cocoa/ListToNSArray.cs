using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch.Cocoa
{
	public static class ListToNSArray
	{
		public static NSArray ToNSArray<T>(this IList<T> items) where T : NSObject
		{
			return NSArray.FromObjects (items.ToArray ());
		}

		public static NSArray ToNSStringArray(this IEnumerable<string> items)
		{
			return NSArray.FromObjects (items.ToArray ());
		}
	}
}

