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
			_items.Add (new Anime ("Foo", "20"));
			_items.Add (new Anime ("Bar", "25"));
			_items.Add (new Anime ("Buz", "30"));
			_items.Add (new Anime ("Nge", "26"));
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

		public Anime (string title, string episodes)
		{
			Title = title;
			Episodes = episodes;
		}

		[Export("copyWithZone:")]
		public virtual NSObject CopyWithZone(IntPtr zone)
		{
			return new Anime(Title, Episodes);
		}
	}
}

