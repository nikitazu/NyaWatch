using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public class SimpleParser : IParser
	{
		public Dictionary<string, string> ParseAnime (TextReader reader)
		{
			var doc = new HtmlDocument ();
			doc.Load (reader);

			var nodes = doc.DocumentNode.SelectNodes ("//div[@class=\"animes\"]/table/tr/td");
			if (nodes == null) {
				return null;
			}

			var result = new Dictionary<string, string> ();

			result ["title"] = nodes [0].InnerText;
			result ["episodes"] = nodes [1].InnerText;

			return result;
		}

		public IList<Dictionary<string, string>> ParseAnimePreview (TextReader reader)
		{
			throw new NotImplementedException ();
		}
	}
}

