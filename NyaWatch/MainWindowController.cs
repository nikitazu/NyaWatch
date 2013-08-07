using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

using cd = NyaWatch.Core.Domain;

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

		#region Init

		public ViewModel.Root Root { get; set; }

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			LoadAwesomeFont ();
			NSUserDefaults.StandardUserDefaults ["NSInitialToolTipDelay"] = NSNumber.FromInt32 (500);

			Root = new ViewModel.Root ();

			animesArrayController.AddObjects (NSArray.FromObjects (Root.Animes.ToArray ()));
		}

		void LoadAwesomeFont()
		{
			categoryPlanToWatchButton.Font = FontAwesome.Font;
			categoryWatchingButton.Font = FontAwesome.Font;
			categoryCompletedButton.Font = FontAwesome.Font;
			categoryOnHoldButton.Font = FontAwesome.Font;
			categoryDroppedButton.Font = FontAwesome.Font;

			categoryPlanToWatchButton.Title = Core.Fonts.Awesome.FastForwardIcon;
			categoryWatchingButton.Title = Core.Fonts.Awesome.PlayIcon;
			categoryCompletedButton.Title = Core.Fonts.Awesome.StopIcon;
			categoryOnHoldButton.Title = Core.Fonts.Awesome.PauseIcon;
			categoryDroppedButton.Title = Core.Fonts.Awesome.EjectIcon;
		}

		#endregion

		#region Category buttons click events

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

		#endregion
	}
}

