using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

using NyaWatch.Cocoa;
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

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			LoadAwesomeFont ();
			NSUserDefaults.StandardUserDefaults ["NSInitialToolTipDelay"] = NSNumber.FromInt32 (500);
			LoadAnimes (cd.Categories.Watching);
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
			LoadAnimes (cd.Categories.PlanToWatch);
		}

		partial void categoryWatchingAction(NSObject sender)
		{
			LoadAnimes (cd.Categories.Watching);
		}

		partial void categoryCompletedAction(NSObject sender)
		{
			LoadAnimes (cd.Categories.Completed);
		}

		partial void categoryOnHoldAction(NSObject sender)
		{
			LoadAnimes (cd.Categories.OnHold);
		}

		partial void categoryDroppedAction(NSObject sender)
		{
			LoadAnimes (cd.Categories.Dropped);
		}

		void LoadAnimes(cd.Categories cat)
		{
			var animes = cd.Anime.Find<ViewModel.Anime> (cat);
			animesArrayController.SetArrangedObjects (animes);
		}

		#endregion
	}
}

