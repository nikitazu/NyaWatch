using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch.Cocoa
{
	/// <summary>
	/// Simplify access to NSArrayController object.
	/// </summary>
	public static class NSArrayControllerExtension
	{
		/// <summary>
		/// Get all items in the specified controller.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> Items<T>(this NSArrayController controller)
		{
			return controller.ArrangedObjects ().Cast<T> ();
		}

		/// <summary>
		/// Sets the arranged objects.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="items">Items.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void SetItems<T>(this NSArrayController controller, IEnumerable<T> items) 
			where T : NSObject
		{
			var arr = items.ToArray ();
			controller.Remove (controller.ArrangedObjects ());
			controller.AddObjects (NSArray.FromNSObjects (arr));
			if (arr.Length > 0) {
				controller.SelectedObjects = new NSObject[] { arr[0] };
			} else {
				controller.SelectedObjects = new NSObject[] { };
			}
		}
	}
}

