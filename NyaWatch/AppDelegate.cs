using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace NyaWatch
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}

		partial void incrementWatchedAction(NSObject sender)
		{
			mainWindowController.incrementWatchedAction(sender);
		}

		partial void decrementWatchedAction(NSObject sender)
		{
			mainWindowController.decrementWatchedAction(sender);
		}

		partial void togglePinnedAction(NSObject sender)
		{
			mainWindowController.togglePinnedAction(sender);
		}
		
		partial void moveToPlanAction(NSObject sender)
		{
			mainWindowController.moveToPlanAction(sender);
		}

		partial void moveToWatchingAction(NSObject sender)
		{
			mainWindowController.moveToWatchingAction(sender);
		}

		partial void moveToCompletedAction(NSObject sender)
		{
			mainWindowController.moveToCompletedAction(sender);
		}

		partial void moveToOnHoldAction(NSObject sender)
		{
			mainWindowController.moveToOnHoldAction(sender);
		}

		partial void moveToDroppedAction(NSObject sender)
		{
			mainWindowController.moveToDroppedAction(sender);
		}


		partial void saveAction(NSObject sender)
		{
			mainWindowController.saveAction(sender);
		}

		partial void openAction(NSObject sender)
		{
			mainWindowController.openAction(sender);
		}

		partial void dropAction(NSObject sender)
		{
			mainWindowController.dropAction(sender);
		}
	}
}

