using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	public class Anime : NSObject
	{
		[Export("title")]
		public string Title { get; set; }

		[Export("episodes")]
		public int Episodes { get; set; }

		[Export("watched")]
		public int Watched { get; set; }

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

		public Anime (string title, string type, int episodes, int torrents, string status)
		{
			Title = title;
			Type = type;
			Episodes = episodes;
			TorrentsCount = torrents;
			Status = status;
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			return new Anime(Title, Type, Episodes, TorrentsCount, Status);
		}

		public override NSObject ValueForUndefinedKey (NSString key)
		{
			Console.WriteLine ("Unknown key: {0}", key);
			return null;
		}
	}
}

