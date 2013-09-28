using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace NyaWatch.Core.Parsing
{
	public class NyaaTorrentParser : TorrentParser, ITorrentParser
	{
		static readonly Regex _descriptionRe = 
			new Regex (@"(\d+) seeder\(s\), (\d+) leecher\(s\), (\d+) download\(s\) - (.+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		protected override List<Dictionary<string, string>> ParseTorrentsFromXml (XDocument xml)
		{
			var torrents = new List<Dictionary<string, string>> ();
			var channel = xml.Root.Element ("channel");
			if (channel == null) {
				throw new FormatException ("Xml in rss format should have channel");
			}

			var rssItems = channel.Elements ("item");
			foreach (var item in rssItems) {
				var torrent = new Dictionary<string, string> ();

				torrent ["title"] = item.Element ("title").Value;
				torrent ["category"] = ParseCategory( item.Element ("category").Value ).ToString();

				// <![CDATA[17 seeder(s), 4 leecher(s), 305 download(s) - 454.5 MiB]]>
				var description = item.Element ("description").Value;
				var match = _descriptionRe.Match (description);
				if (match.Success) {
					torrent ["seeders"] = match.Groups [1].Value;
					torrent ["leechers"] = match.Groups [2].Value;
					torrent ["downloads"] = match.Groups [3].Value;
					torrent ["size"] = match.Groups [4].Value;
				}

				torrents.Add (torrent);
			}

			return torrents;
		}

		Domain.TorrentCategory ParseCategory(string categoryName)
		{
			switch (categoryName) {
			case "Raw Anime": return Domain.TorrentCategory.Raw;
			case "English-translated Anime": return Domain.TorrentCategory.English;
			case "Non-English-translated Anime": return Domain.TorrentCategory.NonEnglish;
			case "Lossless Audio": return Domain.TorrentCategory.Ost;
			case "Lossy Audio": return Domain.TorrentCategory.Ost;
			default:
				return Domain.TorrentCategory.Else;
			}
		}
	}
}

