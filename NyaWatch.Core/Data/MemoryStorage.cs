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

		public void addCategory (string category)
		{
			if (!checkCategoryExistence (category)) {
				_db.Add (category, new CategoryAlias ());
			}
		}

		public void removeCategory (string category)
		{
			_db.Remove (category);
		}

		public bool checkCategoryExistence (string category)
		{
			return _db.ContainsKey (category);
		}

		public Guid addItem (string category, Dic item)
		{
			if (!checkCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var id = Guid.NewGuid ();

			_db [category][id] = item;

			return id;
		}

		public void removeItem(string category, Guid id)
		{
			if (!checkCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var cat = _db [category];

			if (id == Guid.Empty) {
				cat.Clear ();
			} else {
				cat.Remove (id);
			}
		}

		public void updateItem(string category, Guid id, Dic values)
		{
			if (!checkCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}
			if (id == Guid.Empty) {
				return;
			}
			if (values == null) {
				return;
			}
			var item = _db [category] [id];
			foreach (var kv in values) {
				item [kv.Key] = kv.Value;
			}
		}

		public IEnumerable<KeyValuePair<Guid, Dic>> selectItems (string category)
		{
			if (!checkCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}
			return _db [category];
		}

		#endregion
	}
}

