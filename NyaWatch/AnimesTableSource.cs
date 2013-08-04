using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	[Register ("AnimesTableSource")]
	public class AnimesTableSource : NSTableViewDataSource
	{
		List<NSObject> _items;

		public AnimesTableSource ()
		{
			_items = new List<NSObject> ();
			_items.Add (new Anime ("Slayers: Excellent", "OVA", "20"));
			_items.Add (new Anime ("Asura", "Movie", "25"));
			_items.Add (new Anime ("Slayers", "TV", "30"));
			_items.Add (new Anime ("Neon Genesis Evangelion", "TV", "26"));
		}

		/// <summary>
		/// Numbers the of rows in table view.
		/// </summary>
		/// <returns>The of rows in table view.</returns>
		/// <param name="table">Table.</param>
		[Export ("numberOfRowsInTableView:")]
		public int NumberOfRowsInTableView(NSTableView table)
		{
			//Console.WriteLine("numberOfRowsInTableView:");
			return _items.Count();
		}

		/// <summary>
		/// Objects the value for table column.
		/// </summary>
		/// <returns>The value for table column.</returns>
		/// <param name="table">Table.</param>
		/// <param name="col">Col.</param>
		/// <param name="row">Row.</param>
		[Export ("tableView:objectValueForTableColumn:row:")]
		public NSObject ObjectValueForTableColumn(NSTableView table, NSTableColumn col, int row)
		{
			//Console.WriteLine("tableView:objectValueForTableColumn:row:");
			return _items [row];
		}
	}

	
	public class Anime : NSObject
	{
		[Export("title")]
		public string Title { get; set; }

		[Export("episodes")]
		public string Episodes { get; set; }

		[Export("type")]
		public string Type { get; set; }

		[Export("typeColor")]
		public NSColor TypeColor {
			get {
				switch (Type) {
				case "TV":
					return NSColor.Blue;
				case "OVA":
					return NSColor.Orange;
				case "Movie":
					return NSColor.Red;
				default:
					return NSColor.Black;
				}
			}
		}

		public Anime (string title, string type, string episodes)
		{
			Title = title;
			Type = type;
			Episodes = episodes;
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			return new Anime(Title, Type, Episodes);
		}

		public override NSObject ValueForUndefinedKey (NSString key)
		{
			Console.WriteLine ("Unknown key: {0}", key);
			return null;
		}
	}
}

