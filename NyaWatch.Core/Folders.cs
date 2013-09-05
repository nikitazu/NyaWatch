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
		/// Initializes the <see cref="NyaWatch.Core.Folders"/> class.
		/// </summary>
		static Folders ()
		{
			var dataPath = Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData);

			Documents = Path.Combine (dataPath, "NyaWatch");
			if (!Directory.Exists (Documents)) {
				Directory.CreateDirectory (Documents);
			}
		}
	}
}

