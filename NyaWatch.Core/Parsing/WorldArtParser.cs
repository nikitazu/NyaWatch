using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public class WorldArtParser : IParser
	{
		public WorldArtParser ()
		{
		}

		#region IParser implementation

		public Dictionary<string, string> ParseAnime (TextReader reader)
		{
			var doc = new HtmlDocument ();
			doc.Load (reader);

			var contentTable = doc.DocumentNode.SelectNodes ("//html/body//table")
				.FirstOrDefault (table => {
					var header = table.SelectNodes("//table/tr/td[@class=\"bg2\"]");
					if (header == null) {
						return false;
					}
					var info = header.FirstOrDefault(
						h => !string.IsNullOrWhiteSpace(h.InnerText) &&
						h.InnerHtml.Contains("Основная информация"));
					return info != null;
			});

			if (contentTable == null) {
				throw new ParserException ("//html/body//table | //table/tr/td[@class=\"bg2\"]");
			}

			var dataTable = contentTable.SelectNodes ("//table")
				.FirstOrDefault (table => {
					var links = table.SelectNodes("tr/td/a");
					return links != null && links.Any(a => {
						return a.Attributes["href"].Value.Contains(
							@"http://www.world-art.ru/animation/animation_poster.php?id=");
					});
			});
			if (dataTable == null) {
				throw new ParserException ("... | //table/tr/td/a");
			}

			return new Dictionary<string, string> ();
		}

		public IList<Dictionary<string, string>> ParseAnimePreview (TextReader reader)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

