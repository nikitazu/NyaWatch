using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Dic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data
{
	public class XmlStorage : MemoryStorage, IStorageOnDisk
	{
		string _path;

		public XmlStorage (string path)
		{
			_path = path;
		}

		#region IStorageOnDisk implementation

		public void Save ()
		{
			var xdb = new XElement ("db");
			var xcategories = new XElement ("categories");
			xdb.Add (xcategories);

			foreach (var category in _db) {
				var categoryName = category.Key;
				var xcategory = new XElement (categoryName);
				xcategories.Add (xcategory);

				foreach (var item in category.Value) {
					var xitem = XItemWithID (item.Key);
					xcategory.Add (xitem);

					foreach (var kv in item.Value) {
						xitem.Add (new XElement (kv.Key, kv.Value));
					}
				}
			}

			var xdoc = new XDocument (xdb);
			xdoc.Save (_path);
		}

		public void Load ()
		{
			var xdoc = XDocument.Load (_path);
			_db.Clear ();
			foreach (var xcategory in xdoc.Root.Element("categories").Elements()) {
				var category = xcategory.Name.ToString();
				AddCategory (category);

				foreach (var xitem in xcategory.Elements()) {
					var item = new Dictionary<string, string> ();
					foreach (var kv in xitem.Elements()) {
						item.Add (kv.Name.ToString(), kv.Value);
					}
					AddItem (category, item, GetXItemID(xitem));
				}
			}
		}

		public bool Exists()
		{
			return File.Exists (_path);
		}

		public void Drop()
		{
			foreach (var category in _db) {
				category.Value.Clear ();
			}
			if (File.Exists (_path)) {
				File.Delete (_path);
			}
		}

		#endregion

		XElement XItemWithID(Guid id)
		{
			return new XElement ("item", new XAttribute ("id", id));
		}

		Guid GetXItemID(XElement xitem)
		{
			return Guid.Parse (xitem.Attribute ("id").Value);
		}
	}
}

