using System;
using System.Collections.Generic;
using System.Linq;
using Dic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data
{
	public class MemoryStorage : IStorage
	{
		public class CategoryAlias : Dictionary<Guid, Dic>
		{
			// empty
		}

		public class DatabaseAlias : Dictionary<string, CategoryAlias>
		{
			// empty
		}


		DatabaseAlias _db;

		public MemoryStorage ()
		{
			_db = new DatabaseAlias ();
		}

		#region IStorage implementation

		public void AddCategory (string category)
		{
			if (!CheckCategoryExistence (category)) {
				_db.Add (category, new CategoryAlias ());
			}
		}

		public void RemoveCategory (string category)
		{
			_db.Remove (category);
		}

		public bool CheckCategoryExistence (string category)
		{
			return _db.ContainsKey (category);
		}

		public Guid AddItem (string category, Dic item)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var id = Guid.NewGuid ();

			_db [category][id] = item;

			return id;
		}

		public void RemoveItem(string category, Guid id)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var cat = _db [category];

			if (id == Guid.Empty) {
				cat.Clear ();
			} else {
				cat.Remove (id);
			}
		}

		public void UpdateItem(string category, Guid id, Dic values)
		{
			var item = GetItem (category, id);
			if (item == null || values == null) {
				return;
			}
			foreach (var kv in values) {
				item [kv.Key] = kv.Value;
			}
		}

		public IEnumerable<KeyValuePair<Guid, Dic>> SelectItems (string category)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}
			return _db [category];
		}

		public Dic GetItem(string category, Guid id)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			try
			{
				return _db [category] [id];
			} catch (KeyNotFoundException) {
				return null;
			}
		}

		#endregion
	}
}

