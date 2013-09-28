using System;
using System.Collections.Generic;
using System.Linq;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// New torrents found.
	/// </summary>
	public class NewTorrentsEvent : BaseEvent, IEvent
	{
		List<Dictionary<string, string>> _torrents;
		IAnime _anime;

		public NewTorrentsEvent (List<Dictionary<string, string>> torrents, IAnime anime)
		{
			_torrents = torrents;
			_anime = anime;
			AnimeID = anime.ID;
		}

		public override string ToString ()
		{
			var query =
				from torrent in _torrents
				select new {
					Title = torrent ["title"],
					Seeders = int.Parse (torrent ["seeders"]),
					Category = torrent["category"]
			};
			var torrents = query.OrderByDescending (t => t.Seeders).Select (
				t => string.Format (
					"{0} s:{1}: {2}", 
					t.Category, 
					t.Seeders, 
					t.Title));

			return string.Format ("[New torrents {0} - <{1}>]\n\t{2}", 
			                      _anime.Title, 
			                      Title, 
			                      string.Join ("\n\t", torrents));
		}
	}
}
