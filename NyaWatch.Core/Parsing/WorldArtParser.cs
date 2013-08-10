using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
				throw new ParserException ("//html/body//table//table/tr/td[@class=\"bg2\"]");
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
				throw new ParserException ("//table/tr/td/a");
			}

			var result = new Dictionary<string, string> ();

			try 
			{
				var otherTitles = dataTable.SelectSingleNode ("tr[2]/td[3]").OuterHtml									// property otherTitles
					.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries)								// --------------------
					.Where(title => !title.Contains("<"))
					.Select(title => HtmlEntity.DeEntitize(title))
					.ToList();

				result ["otherTitles"] = string.Join(",", otherTitles);
			} catch (Exception) {
				result ["otherTitles"] = string.Empty;
			}

			var fonts = dataTable.SelectNodes ("tr//td//font");
			if (fonts == null || fonts.Count < 4) {
				throw new ParserException ("tr/td/font");
			}

			var typeAndSeries = HtmlEntity.DeEntitize (fonts [3].InnerText);					
			var countryRe = new Regex (@"Производство:\s*(.+)Жанр:", RegexOptions.IgnoreCase | RegexOptions.Compiled);	// property country
			var countryM = countryRe.Match (typeAndSeries);																// ----------------
			result ["country"] = countryM.Success ? countryM.Groups [1].Value : string.Empty;

			result ["title"] = fonts [0].InnerText.Replace (" [", string.Empty);										// property title
			result ["year"] = fonts [1].InnerText;																		// property year

			return result;
		}

		public IList<Dictionary<string, string>> ParseAnimePreview (TextReader reader)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

