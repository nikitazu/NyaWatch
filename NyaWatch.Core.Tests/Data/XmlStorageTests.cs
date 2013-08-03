using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NyaWatch.Core.Data.Tests
{
	[TestFixture]
	public class XmlStorageTests
	{
		[Test]
		public void TestCreateCheckDropStorage ()
		{
			var storage = new XmlStorage<MyTestModel> ();
			Assert.DoesNotThrow (() => storage.DropStorage (), "Drop failed, but it should work allways");

			bool databaseExists = false;
			Assert.DoesNotThrow (() => databaseExists = storage.CheckStorageExistence (), "Check storage existence failed");
			Assert.IsFalse (databaseExists, "We dropped database, so it should not exist");

			Assert.DoesNotThrow (() => storage.CreateStorage (), "Create storage failed");

			Assert.IsTrue (storage.CheckStorageExistence ());
			Assert.Throws<InvalidOperationException>(() => storage.CreateStorage());
			Assert.DoesNotThrow (() => storage.DropStorage ());
			Assert.DoesNotThrow (() => storage.DropStorage ());
			Assert.False (storage.CheckStorageExistence ());
		}

		[Test]
		public void TestInsert()
		{
			var storage = new XmlStorage<MyTestModel> ();
			storage.DropStorage();
			storage.CreateStorage();

			var item = new MyTestModel {
				Name = "Hello"
			};

			Guid id = Guid.Empty;
			Assert.DoesNotThrow (() => id = storage.Insert ("qq", item), "Insert item failed");
			Assert.AreNotEqual (id, Guid.Empty, "ID not returned");
			Assert.AreNotEqual (item.ID, Guid.Empty, "ID not initialized in new item");
			Assert.AreEqual (id, item.ID, "ID initialized in new item not same as returned one");
		}

		[Test]
		public void TestSelectInsert()
		{
			var storage = new XmlStorage<MyTestModel> ();
			storage.DropStorage();
			storage.CreateStorage();

			var emptyItems = storage.Select ("qq", _ => true);
			Assert.NotNull (emptyItems, "Select collection not initialized");
			Assert.AreEqual (0, emptyItems.Count (), "Empty collection returned data");

			var item = new MyTestModel {
				Name = "Hello"
			};

			Guid id = storage.Insert ("qq", item);
			var items = storage.Select("qq", it => it.ID == id);
			Assert.NotNull (items, "Select collection not initialized");
			Assert.AreEqual (1, items.Count (), "Items not found");

			var resultItem = items.Single ();
			Assert.AreEqual (resultItem.ID, item.ID);
			Assert.AreEqual (resultItem.Name, item.Name);
		}

		[Test]
		public void TestUpdate()
		{
			var storage = new XmlStorage<MyTestModel> ();
			storage.DropStorage ();
			storage.CreateStorage ();

			var item = new MyTestModel { Name = "Hello" };

			storage.Insert("qq", new MyTestModel { Name = "noize" });
			Guid id = storage.Insert ("qq", item);
			storage.Insert("qq", new MyTestModel { Name = "noize2" });

			item.Name = "Bye";
			storage.Update ("qq", item);

			var resultItem = storage.Select ("qq", it => it.ID == id).FirstOrDefault ();
			Assert.NotNull (resultItem, "Saved item not found");
			Assert.AreEqual (item.Name, resultItem.Name, "New name not saved");
		}

		[Test]
		public void TestDelete()
		{
			var storage = new XmlStorage<MyTestModel> ();
			storage.DropStorage ();
			storage.CreateStorage ();

			var items = storage.Select ("qq", _ => true);
			Assert.AreEqual (0, items.Count (), "There should be no items on init");

			var a = new MyTestModel () { Name = "a" };
			var b = new MyTestModel () { Name = "b" };
			var c = new MyTestModel () { Name = "c" };

			storage.Insert ("qq", a);
			storage.Insert ("qq", b);
			storage.Insert ("qq", c);

			Assert.AreEqual (3, storage.Select ("qq", _ => true).Count (), "Inserted items not found");
			
			storage.Delete ("qq", a);
			Assert.AreEqual (0, storage.Select ("qq", it => it.ID == a.ID).Count (), "A should be deleted");

			storage.Delete ("qq", b);
			Assert.AreEqual (0, storage.Select ("qq", it => it.ID == b.ID).Count (), "B should be deleted");

			storage.Delete ("qq", c);
			Assert.AreEqual (0, storage.Select ("qq", it => it.ID == c.ID).Count (), "C should be deleted");
		}
	}

	public class MyTestModel : IEntity
	{
		public Guid ID { get; set; }
		public string Name { get; set; }
	}
}

