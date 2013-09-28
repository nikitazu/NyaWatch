using System;
using System.Collections.Generic;

namespace NyaWatch.Core.Domain
{
	/// <summary>
	/// Interface of a torrent.
	/// </summary>
	public interface ITorrent
	{
		/// <summary>
		/// Gets or sets the raw title.
		/// </summary>
		/// <value>The raw title.</value>
		string RawTitle { get; set; }

		/// <summary>
		/// Gets or sets the clean title.
		/// </summary>
		/// <value>The clean title.</value>
		string CleanTitle { get; set; }

		/// <summary>
		/// Gets or sets the seeders.
		/// </summary>
		/// <value>The seeders.</value>
		int Seeders { get; set; }

		/// <summary>
		/// Gets or sets the leechers.
		/// </summary>
		/// <value>The leechers.</value>
		int Leechers { get; set; }

		/// <summary>
		/// Gets or sets the release group.
		/// </summary>
		/// <value>The release group.</value>
		string ReleaseGroup { get; set; }
	}
}
