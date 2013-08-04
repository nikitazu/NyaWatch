using System;
using System.Collections.Generic;

using Dic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data
{
	public interface IStorage
	{
		/// <summary>
		/// Adds the category to database.
		/// </summary>
		/// <param name="category">Category.</param>
		void addCategory(string category);

		/// <summary>
		/// Removes the category from database.
		/// </summary>
		/// <param name="category">Category.</param>
		void removeCategory(string category);

		/// <summary>
		/// Checks the category existence in database.
		/// </summary>
		/// <returns>Category exists.</returns>
		/// <param name="category">Category.</param>
		bool checkCategoryExistence(string category);

		/// <summary>
		/// Adds the item to category.
		/// </summary>
		/// <returns>The item ID.</returns>
		/// <exception cref="CategoryNotFoundException"></exception>
		/// <param name="category">Category.</param>
		/// <param name="item">Item.</param>
		Guid addItem(string category, Dic item);

		/// <summary>
		/// Removes the item by id.
		/// </summary>
		/// <param name="category">Category.</param>
		/// <param name="id">Id.</param>
		void removeItem(string category, Guid id);

		/// <summary>
		/// Updates item with id with provided values.
		/// </summary>
		/// <param name="category">Category.</param>
		/// <param name="id">Id.</param>
		/// <param name="values">Values.</param>
		void updateItem(string category, Guid id, Dic values);

		/// <summary>
		/// Selects all items in category.
		/// </summary>
		/// <returns>The items.</returns>
		/// <param name="category">Category.</param>
		IEnumerable<KeyValuePair<Guid, Dic>> selectItems (string category);
	}
}

