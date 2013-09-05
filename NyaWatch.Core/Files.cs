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
	}
}

