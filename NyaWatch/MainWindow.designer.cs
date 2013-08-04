// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace NyaWatch
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		MonoMac.AppKit.NSArrayController animesArrayController { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTableView animesTable { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton categoryCompletedButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton categoryDroppedButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton categoryOnHoldButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton categoryPlanToWatchButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSButton categoryWatchingButton { get; set; }

		[Action ("categoryCompletedAction:")]
		partial void categoryCompletedAction (MonoMac.Foundation.NSObject sender);

		[Action ("categoryDroppedAction:")]
		partial void categoryDroppedAction (MonoMac.Foundation.NSObject sender);

		[Action ("categoryOnHoldAction:")]
		partial void categoryOnHoldAction (MonoMac.Foundation.NSObject sender);

		[Action ("categoryPlanToWatchAction:")]
		partial void categoryPlanToWatchAction (MonoMac.Foundation.NSObject sender);

		[Action ("categoryWatchingAction:")]
		partial void categoryWatchingAction (MonoMac.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (animesArrayController != null) {
				animesArrayController.Dispose ();
				animesArrayController = null;
			}

			if (categoryCompletedButton != null) {
				categoryCompletedButton.Dispose ();
				categoryCompletedButton = null;
			}

			if (categoryDroppedButton != null) {
				categoryDroppedButton.Dispose ();
				categoryDroppedButton = null;
			}

			if (categoryOnHoldButton != null) {
				categoryOnHoldButton.Dispose ();
				categoryOnHoldButton = null;
			}

			if (categoryPlanToWatchButton != null) {
				categoryPlanToWatchButton.Dispose ();
				categoryPlanToWatchButton = null;
			}

			if (categoryWatchingButton != null) {
				categoryWatchingButton.Dispose ();
				categoryWatchingButton = null;
			}

			if (animesTable != null) {
				animesTable.Dispose ();
				animesTable = null;
			}
		}
	}

	[Register ("MainWindow")]
	partial class MainWindow
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
