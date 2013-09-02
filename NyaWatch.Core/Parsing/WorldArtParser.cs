using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public class WorldArtParser : Parser, IParser
	{
		public WorldArtParser ()
		{
		}

		#region IParser implementation

		protected override Dictionary<string, string> ParseAnime (HtmlDocument doc)
		{
			var nodes = doc.DocumentNode.SelectNodes ("//html/body//table");
			if (nodes == null) {
				throw new ParserException (
					"//html/body//table", 
					doc.DocumentNode.InnerHtml, 
					"contentTableNodes");
			}

			string phase = "contentTable:header search";
			var contentTable = nodes.FirstOrDefault (table => {
				var header = table.SelectNodes("//table/tr/td[@class=\"bg2\"]");
				if (header == null) {
					return false;
				}
				var info = header.FirstOrDefault(
					h => !string.IsNullOrWhiteSpace(h.InnerText) &&
					h.InnerHtml.Contains("Основная информация"));
				phase = "contentTable:main info search";
				return info != null;
			});

			if (contentTable == null) {
				throw new ParserException (
					"//html/body//table//table/tr/td[@class=\"bg2\"]", 
					doc.DocumentNode.InnerHtml, 
					phase);
			}

			var dataTable = contentTable.SelectNodes ("//table")
				.LastOrDefault (table => {
					var links = table.SelectNodes("tr/td/a");
					return links != null && links.Any(a => {
						return a.Attributes["href"].Value.Contains(
							@"http://www.world-art.ru/animation/animation_poster.php?id=");
					});
			});
			if (dataTable == null) {
				throw new ParserException ("//table/tr/td/a", contentTable.InnerHtml, "dataTable");
			}

			var result = new Dictionary<string, string> ();

			phase = "property:otherTitles";
			var otherTitles = dataTable.SelectSingleNode ("tr[2]/td[3]");
			if (otherTitles == null) {
				throw new ParserException ("tr[2]/td[3]", dataTable.InnerHtml, phase);
			}

			try 
			{
				var temp = otherTitles.OuterHtml
					.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries)
					.Where(title => !title.Contains("<"))
					.Select(title => HtmlEntity.DeEntitize(title))
					.ToList();

				result ["otherTitles"] = string.Join(",", temp);
			} catch (Exception) {
				result ["otherTitles"] = string.Empty;
			}

			var fonts = dataTable.SelectNodes ("tr//td//font");
			if (fonts == null || fonts.Count < 4) {
				throw new ParserException ("tr/td/font", fonts == null ? "NULL" : fonts.ToString(), "font tags with data");
			}

			result ["title"] = fonts [0].InnerText.Replace (" [", string.Empty);										// property title
			result ["year"] = fonts [1].InnerText;																		// property year

			var typeAndSeries = HtmlEntity.DeEntitize (fonts [3].InnerText);					
			var countryRe = new Regex (@"Производство:\s*(.+)Жанр:", RegexOptions.IgnoreCase | RegexOptions.Compiled);	// property country
			var countryM = countryRe.Match (typeAndSeries);																// ----------------
			result ["country"] = countryM.Success ? countryM.Groups [1].Value : string.Empty;

			var type = string.Empty;																					// property type
			var episodes = string.Empty;																				// property episodes
			var time = string.Empty;
			var typeAndSeriesRe = new Regex (@"Тип: (.+), (\d+) мин\.(Выпуск|Премьера):", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			var typeAndSeriesSimpleRe = new Regex (@"Тип: (.+)(Выпуск|Премьера):", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			var typeAndSeriesM = typeAndSeriesRe.Match (typeAndSeries);

			if (typeAndSeriesM.Success) {
				type = typeAndSeriesM.Groups [1].Value;
				time = typeAndSeriesM.Groups [2].Value;
				if (type.Contains ("фильм")) {
					type = "Movie";
					episodes = "1";
				} else {
					var tsRe = new Regex (@"(\w+) \((\d+) эп.\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					var tsM = tsRe.Match (type);
					if (tsM.Success) {
						type = tsM.Groups [1].Value;
						episodes = tsM.Groups [2].Value;
						if (type == "ТВ") {
							type = "TV";
						}
					}
				}
			} else {
				var typeAndSeriesSimpleM = typeAndSeriesSimpleRe.Match (typeAndSeries);
				if (typeAndSeriesSimpleM.Success) {
					type = typeAndSeriesSimpleM.Groups [1].Value;
					if (type.Contains ("фильм")) {
						type = "Movie";
						episodes = "1";
					}
				}
			}

			result ["type"] = type;
			result ["episodes"] = episodes;

			// property airingStart
			// property airingEnd
			var airingRe = new Regex (@"Выпуск: c (\d\d\.\d\d\.\d\d\d\d) по  (\d\d\.\d\d\.\d\d\d\d)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			var airingM = airingRe.Match (typeAndSeries);
			if (!airingM.Success) {
				//File.WriteAllText ("/Users/nikitazu/test", typeAndSeries); crazy unseen character near "по" must be copied
				airingRe = new Regex (@"Выпуск: с (\d\d\.\d\d\.\d\d\d\d)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
				airingM = airingRe.Match (typeAndSeries);
				if (!airingM.Success) {
					airingRe = new Regex (@"Премьера: (\d\d\.\d\d\.\d\d\d\d)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					airingM = airingRe.Match (typeAndSeries);
				}
			}

			if (airingM.Success) {
				result ["airingStart"] = airingM.Groups [1].Value;
				result ["airingEnd"] = airingM.Groups.Count == 3 ? airingM.Groups [2].Value : string.Empty;
			} else {
				result ["airingStart"] = string.Empty;
				result ["airingEnd"] = string.Empty;
			}

			var image = dataTable.SelectSingleNode ("tr/td/a/img[1]");
			var imageUrl = image != null ? image.Attributes ["src"].Value : string.Empty;
			var poster = dataTable.SelectSingleNode ("tr/td/a[1]");
			var posterUrl = poster != null ? poster.Attributes ["href"].Value : string.Empty;

			result ["imageUrl"] = imageUrl;
			result ["posterUrl"] = posterUrl;

			return result;
		}

		protected override Dictionary<string, string> ParseAnimePreview (HtmlDocument doc)
		{
			throw new NotImplementedException ("ParseAnimePreview");
		}

		#endregion
	}
}

