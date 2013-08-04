using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Dic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data.Tests
{
	[TestFixture()]
	public class MemoryStorageTests
	{
		IStorage _storage;
		IDictionary<string, string> _a;
		IDictionary<string, string> _b;
		IDictionary<string, string> _c;

		[SetUp]
		public void SetUp()
		{
			_storage = new MemoryStorage ();
			_a = new Dictionary<string, string> ();
			_b = new Dictionary<string, string> ();
			_c = new Dictionary<string, string> ();
			_a ["data"] = "aaa";
			_b ["data"] = "bbb";
			_c ["data"] = "ccc";
		}

		[Test]
		public void TestAddCategory()
		{
			Assert.DoesNotThrow (
				() => _storage.addCategory ("foo"), 
				"Unable to create a category");
		}

		[Test]
		public void TestAddCategoryTwice()
		{
			_storage.addCategory ("foo");

			Assert.DoesNotThrow (
				() => _storage.addCategory ("foo"), 
				"Unable to create a existing category, should do nothing but not fail");
		}

		[Test]
		public void TestCheckCategoryExistente()
		{
			bool result = false;

			Assert.DoesNotThrow (
				() => result = _storage.checkCategoryExistence ("foo"),
				"Unable to check not existing category existence");

			Assert.IsFalse (result, "Category check should return false for not created category");

			_storage.addCategory ("foo");

			Assert.DoesNotThrow (
				() => result = _storage.checkCategoryExistence ("foo"),
				"Unable to check existing category existence");

			Assert.IsTrue (result, "Category check should return true for created category");
		}

		[Test]
		public void TestRemoveCategory()
		{
			_storage.addCategory ("foo");

			Assert.DoesNotThrow (
				() => _storage.removeCategory ("foo"), 
				"Unable to remove a category");

			Assert.IsFalse (
				_storage.checkCategoryExistence ("foo"), 
				"Category should not exist after removal");
		}

		[Test]
		public void TestRemoveNonExistantCategory()
		{
			Assert.DoesNotThrow (
				() => _storage.removeCategory ("foo"),
				"Unable to remove non existant category, should do nothing but not fail");
		}

		[Test]
		public void TestAddItem()
		{
			Assert.Throws<CategoryNotFoundException> (
				() => _storage.addItem ("foo", _a),
				"Category not found exception should be raised");

			_storage.addCategory ("foo");

			Guid id = Guid.Empty;
			Assert.DoesNotThrow (
				() => id = _storage.addItem ("foo", _a),
				"Unable to add item");

			Assert.AreNotEqual (id, Guid.Empty, "Item ID should be generated");

			IEnumerable<KeyValuePair<Guid, Dic>> items = null;
			Assert.DoesNotThrow(
				() => items = _storage.selectItems("foo"),
				"Unable to select items");

			Assert.NotNull (items, "Items not selected");
			var founditem = items.FirstOrDefault();
			Assert.NotNull(founditem, "Added item not found");

			Assert.IsTrue (founditem.Value.ContainsKey("data"), "Initial data key should present");
			Assert.AreEqual ("aaa", founditem.Value["data"], "data should be aaa");
		}

		[Test]
		public void TestSelectItems()
		{
			Assert.Throws<CategoryNotFoundException> (
				() => _storage.selectItems ("foo"),
				"Category not found exception should be thrown");

			_storage.addCategory ("foo");

			Assert.AreEqual (0, _storage.selectItems ("foo").Count (), "No items should be found yet");

			var aid = _storage.addItem ("foo", _a);
			var bid = _storage.addItem ("foo", _b);
			var cid = _storage.addItem ("foo", _c);

			var items = _storage.selectItems ("foo");
			Assert.AreEqual (3, items.Count (), "3 items should be found");

			var itemsList = items.ToDictionary (kv => kv.Key, kv => kv.Value);
			Assert.AreEqual ("aaa", itemsList [aid] ["data"]);
			Assert.AreEqual ("bbb", itemsList [bid] ["data"]);
			Assert.AreEqual ("ccc", itemsList [cid] ["data"]);
		}

		[Test]
		public void TestRemoveItem()
		{
			Assert.Throws<CategoryNotFoundException> (
				() => _storage.removeItem ("foo", Guid.Empty),
				"Category not found exception should be thrown");

			_storage.addCategory("foo");

			Assert.DoesNotThrow (
				() => _storage.removeItem ("foo", Guid.Empty),
				"Nothing should be done but no exception should be thrown");

			var aid = _storage.addItem ("foo", _a);
			var bid = _storage.addItem ("foo", _b);

			Assert.DoesNotThrow (
				() => _storage.removeItem ("foo", bid),
				"Item b should be removed");

			var items = _storage.selectItems ("foo");
			Assert.AreEqual (1, items.Count (), "Only one items should remain");
			Assert.AreEqual (aid,
			                 items.First ().Key,
			                 "Only a should remain");
		}

		[Test]
		public void TestUpdateItem()
		{
			Assert.Throws<CategoryNotFoundException> (
				() => _storage.updateItem ("foo", Guid.Empty, null),
				"Category not found exception should be thrown");

			_storage.addCategory ("foo");

			Assert.DoesNotThrow (
				() => _storage.updateItem ("foo", Guid.Empty, null),
				"Should do nothing and not fail");

			var aid = _storage.addItem ("foo", _a);
			_storage.addItem ("foo", _b);

			Assert.DoesNotThrow (
				() => _storage.updateItem ("foo", aid, null),
				"Should do nothing and not fail");

			var newAValue = new Dictionary<string, string> ();
			newAValue ["data"] = "aax";

			Assert.DoesNotThrow (
				() => _storage.updateItem ("foo", aid, newAValue),
				"Item a should be updated");

			var updatedA = _storage.selectItems ("foo").First (kv => kv.Key == aid);
			Assert.AreEqual ("aax", 
			                 updatedA.Value ["data"],
			                "Item a data should be aax");
		}
	}
}

