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

		public static void SetArrangedObjects<T>(this NSArrayController controller, IEnumerable<T> items) where T :NSObject
		{
			var arr = items.ToArray ();
			controller.Remove (controller.ArrangedObjects ());
			controller.AddObjects (NSArray.FromNSObjects (arr));
			controller.SelectedObjects = new NSObject[] { arr[0] };
		}
	}
}

