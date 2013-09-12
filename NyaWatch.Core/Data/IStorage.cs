using System;
using System.Collections.Generic;

using Dic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data
{
	/// <summary>
	/// Interface for data storage.
	/// </summary>
	public interface IStorage
	{
		/// <summary>
		/// Adds the category to database.
		/// </summary>
		/// <param name="category">Category.</param>
		void AddCategory(string category);

		/// <summary>
		/// Removes the category from database.
		/// </summary>
		/// <param name="category">Category.</param>
		void RemoveCategory(string category);

		/// <summary>
		/// Checks the category existence in database.
		/// </summary>
		/// <returns>Category exists.</returns>
		/// <param name="category">Category.</param>
		bool CheckCategoryExistence(string category);

		/// <summary>
		/// Adds the item to category.
		/// </summary>
		/// <returns>The item ID.</returns>
		/// <exception cref="CategoryNotFoundException"></exception>
		/// <param name="category">Category.</param>
		/// <param name="item">Item.</param>
		Guid AddItem(string category, Dic item);

		/// <summary>
		/// Removes the item by id.
		/// </summary>
		/// <param name="id">Id.</param>
		void RemoveItem(Guid id);

		/// <summary>
		/// Updates item with id with provided values.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <param name="values">Values.</param>
		void UpdateItem(Guid id, Dic values);

		/// <summary>
		/// Moves the item to category.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="category">
		/// The category, where item will be moved.
		/// </param>
		void MoveItem (Guid id, string category);

		/// <summary>
		/// Selects all items in category.
		/// </summary>
		/// <returns>The items.</returns>
		/// <param name="category">Category.</param>
		IEnumerable<KeyValuePair<Guid, Dic>> SelectItems (string category);

		/// <summary>
		/// Gets the item by identifier.
		/// </summary>
		/// <returns>The item.</returns>
		/// <param name="id">Identifier.</param>
		Dic GetItem(Guid id);
	}
}

