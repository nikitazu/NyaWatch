using System;

namespace NyaWatch.Core.Data
{
	/// <summary>
	/// Interface for data storage with ability to save data on disk and load data from disk;
	/// </summary>
	public interface IStorageOnDisk : IStorage
	{
		/// <summary>
		/// Save data.
		/// </summary>
		void Save();

		/// <summary>
		/// Load data.
		/// </summary>
		void Load();

		/// <summary>
		/// Drop database.
		/// </summary>
		void Drop();

		/// <summary>
		/// Does database exist?
		/// </summary>
		bool Exists();
	}
}
