using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors
		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		// Call to load from the XIB/NIB file
		public MainWindowController () : base ("MainWindow")
		{
			Initialize ();
		}
		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion
		//strongly typed window accessor
		public new MainWindow Window {
			get {
				return (MainWindow)base.Window;
			}
		}

		partial void categoryPlanToWatchAction(NSObject sender)
		{
			Console.WriteLine("plan to watch category");
		}

		partial void categoryWatchingAction(NSObject sender)
		{
			Console.WriteLine("watching category");
		}

		partial void categoryCompletedAction(NSObject sender)
		{
			Console.WriteLine("completed category");
		}

		partial void categoryOnHoldAction(NSObject sender)
		{
			Console.WriteLine("on hold category");
		}

		partial void categoryDroppedAction(NSObject sender)
		{
			Console.WriteLine("dropped category");
		}
	}
}

