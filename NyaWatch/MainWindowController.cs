using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

using NyaWatch.Cocoa;
using cd = NyaWatch.Core.Domain;
using ui = NyaWatch.Core.UI;

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

		public cd.IRoot Root { get; set; }

		#region Init

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			LoadAwesomeFont ();
			NSUserDefaults.StandardUserDefaults ["NSInitialToolTipDelay"] = NSNumber.FromInt32 (500);
			Root = new ViewModel.Root ();
			LoadAnimes (cd.Categories.Watching);
			LoadEvents ();
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

		private readonly NyaWatch.Core.Parsing.AnimeUrlResolver _animeUrlResolver =
			new NyaWatch.Core.Parsing.AnimeUrlResolver();

		partial void searchAnimeAction(NSObject sender)
		{
			var searchData = sender.ValueForKey((NSString)"stringValue").ToString();
			if (string.IsNullOrWhiteSpace(searchData)) {
				return;
			}

			var parser = _animeUrlResolver.CreateParserFor(searchData);
			if (parser == null) {
				ui.Dialogs.Message.Error(
					"Incorrect input",
					"Only direct urls to anime are supported for now.\nPlease, provide url like:\n" +
					"http://www.world-art.ru/animation/animation.php?id=395");
			}
			var id = cd.Anime.ParseFromWeb(parser, searchData);
			LoadAnimes(cd.Categories.PlanToWatch);
			cd.Anime.LoadImage(Animes.First(a => a.ID == id));

			// clear text
			sender.SetValueForKey((NSString)"", (NSString)"stringValue");
		}

		IEnumerable<cd.IAnime> Animes {
			get { return animesArrayController.Items<cd.IAnime> (); }
		}

		IEnumerable<cd.Events.IEvent> Events {
			get { return eventsArrayController.Items<cd.Events.IEvent> (); }
		}

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
			var animes = cd.Anime.Find<ViewModel.Anime> (cat).Select (a => (ViewModel.Anime)a.WithRoot (Root));
			animesArrayController.SetItems (animes);
			Root.SelectedCategory = cat;
		}

		void LoadEvents()
		{
			var events = cd.Events.Manager.LoadAll ().Select (evt => new ViewModel.Event (evt));
			eventsArrayController.SetItems (events);
		}

		#endregion

		#region Selected anime actions

		public void incrementWatchedAction(NSObject sender)
		{
			cd.Anime.Increment (SelectedAnime);
		}

		public void decrementWatchedAction(NSObject sender)
		{
			cd.Anime.Decrement (SelectedAnime);
		}

		public void togglePinnedAction(NSObject sender)
		{
			SelectedAnime.TogglePinned ();
		}

		public void loadImageAction(NSObject sender)
		{
			cd.Anime.LoadImage (SelectedAnime);
		}

		public void moveToPlanAction(NSObject sender)
		{
			MoveAnimeTo (cd.Categories.PlanToWatch);
		}

		public void moveToWatchingAction(NSObject sender)
		{
			MoveAnimeTo (cd.Categories.Watching);
		}

		public void moveToCompletedAction(NSObject sender)
		{
			MoveAnimeTo (cd.Categories.Completed);
		}

		public void moveToOnHoldAction(NSObject sender)
		{
			MoveAnimeTo (cd.Categories.OnHold);
		}

		public void moveToDroppedAction(NSObject sender)
		{
			MoveAnimeTo (cd.Categories.Dropped);
		}

		void MoveAnimeTo(cd.Categories cat)
		{
			if (Root.SelectedCategory != cat) {
				cd.Anime.Move (cat, SelectedAnime);
				animesArrayController.RemoveObject (SelectedAnime);
			}
		}

		ViewModel.Anime SelectedAnime
		{
			get { 
				if (animesArrayController.SelectedObjects.Length <= 0) {
					return null;
				}
				return animesArrayController.SelectedObjects [0] as ViewModel.Anime;
			}
		}

		#endregion

		#region Database file actions

		public void saveAction(NSObject sender)
		{
			cd.Anime.Save ();
		}

		public void openAction(NSObject sender)
		{
			try
			{
				cd.Anime.Load ();
			} catch (System.IO.FileNotFoundException ex) {
				ui.Dialogs.Message.Error ("Database open error (not found)", ex);
			}
		}

		public void dropAction(NSObject sender)
		{
			cd.Anime.Drop ();
		}

		#endregion
	}
}

