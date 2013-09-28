using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MonoMac.Foundation;
using MonoMac.AppKit;
using NyaWatch.Cocoa;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.ViewModel
{
	public class Anime : NSObject, cd.IAnime
	{
		public Guid ID { get; set; }

		[Export("title")]
		public string Title { get; set; }

		[Export("otherTitles")]
		public string[] OtherTitlesHelper { get; set; }

		public List<string> OtherTitles {
			get {
				return OtherTitlesHelper.ToList ();
			}
			set {
				SetValueForKey (value.ToNSStringArray(), (NSString)"otherTitles");
			}
		}

		[Export("episodes")]
		public int Episodes { get; set; }

		[Export("watched")]
		public int WatchedHelper { get; set; }

		public int Watched 
		{ 
			get { return WatchedHelper; }
			set { SetValueForKey (NSNumber.FromInt32 (value), (NSString)"watched"); }
		}

		[Export("incrementWatchedAction")]
		public void Increment()
		{
			cd.Anime.Increment (this);
		}

		[Export("decrementWatchedAction")]
		public void Decrement()
		{
			cd.Anime.Decrement (this);
		}

		[Export("incrementIcon")]
		public string incrementIcon
		{
			get { return Core.Fonts.Awesome.PlusIcon; }
		}

		[Export("decrementIcon")]
		public string decrementIcon
		{
			get { return Core.Fonts.Awesome.MinusIcon; }
		}

		[Export("type")]
		public string Type { get; set; }

		[Export("typeColor")]
		public NSColor TypeColor {
			get {
				switch (Type) {
				case "TV":
					return NSColor.Brown;
				case "OVA":
					return NSColor.Orange;
				case "Movie":
					return NSColor.Magenta;
				default:
					return NSColor.Black;
				}
			}
		}

		[Export("torrentsCount")]
		public int TorrentsCount { get; set; }

		[Export("torrentsMessage")]
		public string TorrentsMessage {
			get {
				switch (TorrentsCount) {
				case 0:
					return "No torrents found";
				case 1:
					return "One torrent found";
				default:
					return string.Format ("{0} torrents found", TorrentsCount);
				}
			}
		}

		[Export("torrentsColor")]
		public NSColor TorrentsColor {
			get {
				return TorrentsCount > 0 ? NSColor.Blue : NSColor.LightGray;
			}
		}

		[Export("status")]
		public string StatusHelper { get; set; }

		[Export("statusColor")]
		public NSColor StatusColorHelper { get; set; }


		public string Status {
			get { return StatusHelper; }
			set { SetValueForKey ((NSString)value, (NSString)"status"); }
		}

		public NSColor StatusColor {
			get { return StatusColorHelper; }
			set { SetValueForKey (value, (NSString)"statusColor"); }
		}

		[Export("imagePath")]
		public string ImagePath { get; set; }

		public string ImageUrl { get; set; }

		[Export("pinned")]
		public bool Pinned { get; set; }
		
		public void TogglePinned()
		{
			SetValueForKey (NSNumber.FromBoolean (!Pinned), (NSString)"pinned");
		}

		[Export("moveToCategoryAction")]
		public void moveToCategoryAction(NSObject sender)
		{
			Console.WriteLine ("round: {0}", Title);
		}

		[Export("font")]
		public NSFont Font {
			get { return FontAwesome.Font; }
		}

		DateTime? _airingStart;
		DateTime? _airingEnd;
		int _year;

		public DateTime? AiringStart 
		{ 
			get { return _airingStart; }
			set {
				_airingStart = value;
				UpdateStatus ();
			}
		}

		public DateTime? AiringEnd 
		{ 
			get { return _airingEnd; }
			set {
				_airingEnd = value;
				UpdateStatus ();
			}
		}

		public int Year
		{ 
			get { return _year; }
			set {
				_year = value;
				UpdateStatus ();
			}
		}

		void UpdateStatus()
		{
			Status = cd.AnimeAiringStatus.Calculate (this, DateTime.Today);
			StatusColor = Status == "Airing" ? NSColor.Blue : NSColor.LightGray;
		}

		public cd.IRoot Root { get; set; }
		public cd.IAnime WithRoot(cd.IRoot root)
		{
			Root = root;
			return this;
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			throw new NotImplementedException ();
		}

		public override NSObject ValueForUndefinedKey (NSString key)
		{
			Console.WriteLine ("Unknown key: {0}", key);
			return null;
		}
	}
}

