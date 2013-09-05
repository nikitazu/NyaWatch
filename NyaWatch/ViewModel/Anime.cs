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

		public DateTime? AiringStart { get; set; }
		public DateTime? AiringEnd { get; set; }
		public int Year { get; set; }

		public Anime (string title, string type, int episodes, int torrents, string airingStart, string airingEnd, int year)
		{
			Title = title;
			Type = type;
			Episodes = episodes;
			TorrentsCount = torrents;
			Year = year;

			try
			{
				AiringStart = DateTime.Parse(airingStart);
			} catch (FormatException) {
				AiringStart = null;
			}

			try
			{
				AiringEnd = DateTime.Parse(airingEnd);
			} catch (FormatException) {
				AiringEnd = null;
			}

			Status = cd.AnimeAiringStatus.Calculate (this, DateTime.Today);
		}

		public Anime ()
		{
			// empty
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			return new Anime(
				Title, Type, Episodes, TorrentsCount, 
				AiringStart == null ? string.Empty : AiringStart.ToString(), 
				AiringEnd == null ? string.Empty : AiringEnd.ToString(), 
				Year);
		}

		public override NSObject ValueForUndefinedKey (NSString key)
		{
			Console.WriteLine ("Unknown key: {0}", key);
			return null;
		}
	}
}

