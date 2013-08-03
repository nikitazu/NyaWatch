using System;
using System.Collections.Generic;

namespace NyaWatch.Core.Data
{
	public interface IStorage<T> where T : IEntity
	{
		void CreateStorage();
		void DropStorage();
		bool CheckStorageExistence();

		IEnumerable<T> Select(string category, Predicate<T> filter);
		Guid Insert(string category, T item);
		void Update(string category, T item);
		void Delete(string category, T item);
	}
}

