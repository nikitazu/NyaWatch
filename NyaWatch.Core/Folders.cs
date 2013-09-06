using System;
using System.IO;

namespace NyaWatch.Core
{
	/// <summary>
	/// NyaWatch special folders.
	/// </summary>
	public static class Folders
	{
		/// <summary>
		/// Folder with NyaWatch documents.
		/// </summary>
		public static readonly string Documents;

        /// <summary>
        /// Folder with anime images.
        /// </summary>
        public static readonly string Images;

		/// <summary>
		/// Initializes the <see cref="NyaWatch.Core.Folders"/> class.
		/// </summary>
		static Folders ()
		{
			var dataPath = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);

			Documents = Path.Combine (dataPath, "NyaWatch");
            MakeDirectory(Documents);

            Images = Path.Combine(Documents, "Images");
            MakeDirectory(Images);
		}

        static void MakeDirectory(string path)
        {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }
	}
}

