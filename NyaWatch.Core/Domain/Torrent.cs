using System;
using System.Collections.Generic;
using System.Linq;
using NyaWatch.Core.Data;
using StructureMap;

namespace NyaWatch.Core.Domain
{
	public static class Torrent
	{
		public static List<ITorrent> ParseTorrentsFromWeb(string url)
		{
			var items = new Parsing.NyaaTorrentParser ().ParseTorrentsFromWeb (url);
			var torrents = new List<ITorrent> ();
			foreach (var item in items) {
				var torrent = ObjectFactory.GetInstance<ITorrent> ();
				DeserializeTorrent (item, torrent);
				torrents.Add (torrent);
			}
			return torrents;
		}

		public static TorrentCategory ParseCategory(string value)
		{
			return (TorrentCategory)Enum.Parse (typeof(TorrentCategory), value);
		}

		static void DeserializeTorrent(IDictionary<string, string> item, ITorrent torrent)
		{
			try
			{
				torrent.RawTitle = item.RequireString("title");
				torrent.CleanTitle = Parsing.NameCleaner.Clean(torrent.RawTitle);
				torrent.Seeders = item.RequireInt("seeders");
				torrent.Leechers = item.RequireInt("leechers");
				torrent.ReleaseGroup = item.OptionalString("releaseGroup") ?? string.Empty;
				torrent.Category = ParseCategory(item.RequireString("category"));
			}
			catch (Exception e)
			{
				throw new DeserializeFailedException (item, e);
			}
		}
	}
}

