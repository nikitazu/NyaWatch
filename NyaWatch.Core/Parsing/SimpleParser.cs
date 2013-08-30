using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public class SimpleParser : Parser, IParser
	{
		protected override Dictionary<string, string> ParseAnime (HtmlDocument doc)
		{
			var nodes = doc.DocumentNode.SelectNodes ("//div[@class=\"animes\"]/table/tr/td");
			if (nodes == null) {
				return null;
			}

			var result = new Dictionary<string, string> ();

			result ["title"] = nodes [0].InnerText;
			result ["episodes"] = nodes [1].InnerText;

			return result;
		}

		protected override Dictionary<string, string> ParseAnimePreview (HtmlDocument doc)
		{
			throw new NotImplementedException ();
		}
	}
}

