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
		List<ITorrent> _torrents;
		IAnime _anime;

		public NewTorrentsEvent (List<ITorrent> torrents, IAnime anime)
		{
			_torrents = torrents;
			_anime = anime;
			AnimeID = anime.ID;
		}

		public override string ToString ()
		{
			var torrents = _torrents.OrderByDescending (t => t.Seeders).Select (
				t => string.Format ("s:{0}: {1}", t.Seeders, t.RawTitle));

			return string.Format ("[New torrents {0} - <{1}>]\n\t{2}", 
			                      _anime.Title, 
			                      Title, 
			                      string.Join ("\n\t", torrents));
		}
	}
}
