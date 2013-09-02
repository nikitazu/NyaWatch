using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MonoMac.Foundation;
using MonoMac.AppKit;

using cd = NyaWatch.Core.Domain;

namespace NyaWatch.ViewModel
{
	public class Anime : NSObject, cd.IAnime
	{
		public Guid ID { get; set; }

		[Export("title")]
		public string Title { get; set; }

		[Export("episodes")]
		public int Episodes { get; set; }

		[Export("watched")]
		public int Watched { get; set; }

		[Export("incrementWatchedAction")]
		public void Increment()
		{
			if (Watched < Episodes) {
				SetValueForKey (NSNumber.FromInt32 (Watched + 1), (NSString)"watched");
			}
		}

		[Export("decrementWatchedAction")]
		public void Decrement()
		{
			if (Watched > 0) {
				SetValueForKey (NSNumber.FromInt32 (Watched - 1), (NSString)"watched");
			}
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
		public string Status { get; set; }

		[Export("statusColor")]
		public NSColor StatusColor {
			get {
				return Status == "Airing" ? NSColor.Blue : NSColor.LightGray;
			}
		}

		public string ImagePath { get; set; }

		[Export("actualImagePath")]
		public string ActualImagePath {
			get {
				return ImagePath ?? "NyaWatch.icns";
			}
		}

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

		public string AiringStart { get; set; }
		public string AiringEnd { get; set; }
		public int Year { get; set; }

		public Anime (string title, string type, int episodes, int torrents, string airingStart, string airingEnd, int year)
		{
			Title = title;
			Type = type;
			Episodes = episodes;
			TorrentsCount = torrents;
			AiringStart = airingStart;
			AiringEnd = airingEnd;
			Year = year;

			if (string.IsNullOrWhiteSpace (airingStart)) {
				Status = "Unknown";
				if (year > DateTime.Today.Year) {
					Status = "Not yet aired";
				} else if (year < DateTime.Today.Year) {
					Status = "Aired";
				}
			} else {
				var startDate = DateTime.Parse (airingStart).Date;
				if (startDate > DateTime.Today) {
					Status = "Not yet aired";
				} else {
					Status = "Airing";
					if (!string.IsNullOrWhiteSpace (airingEnd)) {
						var endDate = DateTime.Parse (airingEnd).Date;
						if (endDate <= DateTime.Today) {
							Status = "Aired";
						}
					}
				}
			}
		}

		public Anime ()
		{
			// empty
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			return new Anime(Title, Type, Episodes, TorrentsCount, AiringStart, AiringEnd, Year);
		}

		public override NSObject ValueForUndefinedKey (NSString key)
		{
			Console.WriteLine ("Unknown key: {0}", key);
			return null;
		}
	}
}

