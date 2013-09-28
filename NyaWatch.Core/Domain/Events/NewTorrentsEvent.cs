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
			return string.Format ("[New torrents {0} - <{1}>]\n\t{2}", 
			                      _anime.Title, 
			                      Title, 
			                      string.Join("\n\t", _torrents.Select(t => t["title"])));
		}
	}
}
