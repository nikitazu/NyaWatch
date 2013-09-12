using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace NyaWatch.Core.Data.Tests
{
	[TestFixture]
	public class XmlStorageTests
	{
		IStorageOnDisk _storage;
		Dictionary<string,string> _item1;
		Dictionary<string,string> _item2;
		Dictionary<string,string> _item3;

		const string DbPath = "tempdb.xml";
		const string One = "cat-one";
		const string Two = "cat-two";

		[TestFixtureSetUp]
		//[TestFixtureTearDown]
		public void TestSetup()
		{
			if (File.Exists (DbPath)) {
				File.Delete (DbPath);
			}
		}

		[SetUp]
		public void Setup()
		{
			_storage = new XmlStorage (DbPath);

			_item1 = new Dictionary<string, string> ();
			_item2 = new Dictionary<string, string> ();
			_item3 = new Dictionary<string, string> ();

			_item1 ["name"] = "apple";
			_item2 ["name"] = "orange";
			_item3 ["name"] = "cake";
		}

		[TearDown]
		public void TearDown()
		{
			_storage = null;
			_item1 = null;
			_item2 = null;
			_item3 = null;
		}

		[Test]
		public void TestSaveLoad ()
		{
			Assert.IsFalse (File.Exists (DbPath), "Database should not exist before save");

			_storage.AddCategory (One);
			_storage.AddCategory (Two);

			var id1 = _storage.AddItem (One, _item1);
			var id2 = _storage.AddItem (One, _item2);
			var id3 = _storage.AddItem (Two, _item3);

			Assert.DoesNotThrow (() => _storage.Save (), "Storage save should work");
			Assert.IsTrue (File.Exists (DbPath), "Database should be created by save");

			_storage = new XmlStorage (DbPath);
			Assert.IsFalse (_storage.CheckCategoryExistence (One), "Category one should not exist before load");
			Assert.IsFalse (_storage.CheckCategoryExistence (Two), "Category two should not exist before load");

			Assert.DoesNotThrow (() => _storage.Load (), "Storage load should work");
			Assert.IsTrue (_storage.CheckCategoryExistence (One), "Category one should be loaded");
			Assert.IsTrue (_storage.CheckCategoryExistence (Two), "Category one should be loaded");

			var oneItems = _storage.SelectItems (One).ToList ();
			var twoItems = _storage.SelectItems (Two).ToList ();

			Assert.AreEqual (2, oneItems.Count(), "Should find 2 items in category one");
			Assert.AreEqual (1, twoItems.Count(), "Should find 1 item in category two");

			var item1 = oneItems [0];
			var item2 = oneItems [1];
			var item3 = twoItems [0];

			Assert.AreEqual (_item1, item1.Value, "Item 1 should be the same");
			Assert.AreEqual (_item2, item2.Value, "Item 2 should be the same");
			Assert.AreEqual (_item3, item3.Value, "Item 3 should be the same");

			Assert.AreEqual (id1, item1.Key, "Item 1 ID should be the same");
			Assert.AreEqual (id2, item2.Key, "Item 2 ID should be the same");
			Assert.AreEqual (id3, item3.Key, "Item 3 ID should be the same");

			var item2byIndex = _storage.GetItem (id2);
			Assert.IsNotNull (item2byIndex);
			Assert.AreEqual (_item2, item2byIndex, "Item 2 should be found by index");
		}
	}
}

