using System;
using System.IO;

namespace NyaWatch.Core
{
	/// <summary>
	/// Special files for NyaWatch.
	/// </summary>
	public static class Files
	{
		/// <summary>
		/// Gets the database path.
		/// </summary>
		/// <value>The database path.</value>
		public static string DatabasePath { get; private set; }

		/// <summary>
		/// Initializes the <see cref="NyaWatch.Core.Files"/> class.
		/// </summary>
		static Files ()
		{
			DatabasePath = Path.Combine (Folders.Documents, "database.xml");
		}

        /// <summary>
        /// Get path to anime image file.
        /// </summary>
        /// <param name="anime">Anime.</param>
        /// <returns>Full path.</returns>
        public static string ImagePath(Domain.IAnime anime)
        {
            return Path.Combine(Folders.Images, anime.ID.ToString("N"));
        }
	}
}

