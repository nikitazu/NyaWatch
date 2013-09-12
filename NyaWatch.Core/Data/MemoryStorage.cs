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


		protected DatabaseAlias _db;
		protected Dictionary<Guid, CategoryAlias> _categoryIndex;

		public MemoryStorage ()
		{
			_db = new DatabaseAlias ();
			_categoryIndex = new Dictionary<Guid, CategoryAlias> ();
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
			var cat = _db [category];
			cat[id] = item;
			UpdateIndex (id, cat);

			return id;
		}

		protected internal void AddItem (string category, Dic item, Guid id)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var cat = _db [category];
			cat[id] = item;
			UpdateIndex (id, cat);
		}

		public void RemoveItem(string category, Guid id)
		{
			if (!CheckCategoryExistence (category)) {
				throw new CategoryNotFoundException (category);
			}

			var cat = _db [category];

			if (id == Guid.Empty) {
				foreach (var k in cat.Keys) {
					RemoveIndex (k);
				}
				cat.Clear ();
			} else {
				RemoveIndex (id);
				cat.Remove (id);
			}
		}

		public void UpdateItem(string category, Guid id, Dic values)
		{
			var item = GetItem (id);
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

		public Dic GetItem(Guid id)
		{
			try {
				return _categoryIndex [id] [id];
			} catch (KeyNotFoundException) {
				return null;
			}
		}

		#endregion

		void UpdateIndex(Guid id, CategoryAlias cat)
		{
			_categoryIndex [id] = cat;
		}

		void RemoveIndex(Guid id)
		{
			_categoryIndex.Remove (id);
		}
	}
}

