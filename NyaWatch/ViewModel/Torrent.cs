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
	public class Torrent : NSObject, cd.ITorrent
	{
		[Export("rawTitle")]
		public string RawTitleHelper { get; set; }

		[Export("cleanTitle")]
		public string CleanTitleHelper { get; set; }

		[Export("seeders")]
		public int SeedersHelper { get; set; }

		[Export("leechers")]
		public int LeechersHelper { get; set; }

		[Export("releaseGroup")]
		public string ReleaseGroupHelper { get; set; }

		[Export("category")]
		public string CategoryHelper { get; set; }

		#region ITorrent implementation

		public string RawTitle {
			get { return RawTitleHelper; }
			set { SetValueForKey ((NSString)value, (NSString)"rawTitle"); }
		}

		public string CleanTitle {
			get { return CleanTitleHelper; }
			set { SetValueForKey ((NSString)value, (NSString)"cleanTitle"); }
		}

		public int Seeders {
			get { return SeedersHelper; }
			set { SetValueForKey (NSNumber.FromInt32(value), (NSString)"seeders"); }
		}

		public int Leechers {
			get { return LeechersHelper; }
			set { SetValueForKey (NSNumber.FromInt32(value), (NSString)"leechers"); }
		}

		public string ReleaseGroup {
			get { return ReleaseGroupHelper; }
			set { SetValueForKey ((NSString)value, (NSString)"releaseGroup"); }
		}

		public cd.TorrentCategory Category { 
			get { return cd.Torrent.ParseCategory(CategoryHelper); }
			set { SetValueForKey ((NSString)value.ToString(), (NSString)"Category"); }
		}

		#endregion


	}
}

