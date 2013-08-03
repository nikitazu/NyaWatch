using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace NyaWatch.Core.Data
{
	public class XmlStorage<T> : IStorage<T> where T : IEntity
	{
		XmlSerializer _serializer;
		string _pathToFolder;
		string _pathToFile;

		public XmlStorage ()
		{
			_serializer = new XmlSerializer (typeof(T));
			_pathToFolder = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "NyaWatch");
			_pathToFile = Path.Combine (_pathToFolder, typeof(T).Name + ".xml");
		}

		#region IStorage implementation

		public void CreateStorage()
		{
			if (!Directory.Exists (_pathToFolder)) {
				Directory.CreateDirectory (_pathToFolder);
			}

			if (File.Exists(_pathToFile)) {
				throw new InvalidOperationException("Storage of type " + typeof(T).Name + " allready exists");
			}

			var xdoc = new XDocument (new XElement ("storage", new XAttribute("version", 1)));
			xdoc.Save (_pathToFile);
		}

		public void DropStorage()
		{
			if (File.Exists (_pathToFile)) {
				File.Delete (_pathToFile);
			}
		}

		public bool CheckStorageExistence()
		{
			if (! File.Exists (_pathToFile) ) {
				return false;
			}

			XDocument xdoc = null;

			try
			{
				xdoc = XDocument.Load (_pathToFile);
			} catch (Exception)
			{
				throw new CorruptStorageException(_pathToFile);
			}

			if (xdoc == null || xdoc.Root == null || xdoc.Root.Name != "storage") {
				throw new CorruptStorageException(_pathToFile);
			}

			return true;
		}

		public IEnumerable<T> Select(string category, Predicate<T> filter)
		{
			var doc = XDocument.Load (_pathToFile);
			var xcategory = doc.Root.Elements (category).FirstOrDefault ();
			var result = new List<T> ();
			if (xcategory == null) {
				return result;
			}

			foreach (var xitem in xcategory.Elements("item")) {
				var data = XDocument.Parse (xitem.Value).ToString ();
				var item = (T)_serializer.Deserialize (new StringReader (data));
				if (filter(item)) {
					result.Add(item);
				}
			}

			return result;
		}

		public Guid Insert(string category, T item)
		{
			item.ID = Guid.NewGuid();

			var doc = XDocument.Load (_pathToFile);
			var xcategory = doc.Root.Elements (category).FirstOrDefault ();
			if (xcategory == null) {
				xcategory = new XElement (category);
				doc.Root.Add (xcategory);
			}

			var builder = new System.Text.StringBuilder ();
			using (var writer = new StringWriter(builder)) {
				_serializer.Serialize(writer, item);
			}

			xcategory.Add (new XElement ("item", new XAttribute ("id", item.ID), builder.ToString ()));
			doc.Save (_pathToFile);

			return item.ID;
		}

		public void Update(string category, T item)
		{
			var doc = XDocument.Load (_pathToFile);
			var xcategory = doc.Root.Elements (category).FirstOrDefault ();
			if (xcategory == null) {
				throw new KeyNotFoundException ("Category " + category + " not found");
			}

			var xitem = xcategory.Elements ("item").Where (xit => xit.Attribute ("id").Value == item.ID.ToString()).FirstOrDefault ();
			if (xitem == null ) {
				throw new KeyNotFoundException("Item not found in category " + category);
			}

			var builder = new System.Text.StringBuilder ();
			using (var writer = new StringWriter(builder)) {
				_serializer.Serialize(writer, item);
			}

			xitem.Value = builder.ToString ();
			doc.Save (_pathToFile);
		}

		public void Delete(string category, T item)
		{
			var doc = XDocument.Load (_pathToFile);
			var xcategory = doc.Root.Elements (category).FirstOrDefault ();
			if (xcategory == null) {
				return;
			}

			bool wasRemove = false;
			foreach (var xitem in xcategory.Elements("item")) {
				if (xitem.Attribute ("id").Value == item.ID.ToString ()) {
					xitem.Remove ();
					wasRemove = true;
				}
			}

			if (wasRemove) {
				doc.Save (_pathToFile);
			}
		}

		#endregion
	}
}

