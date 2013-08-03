using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace NyaWatch.Core.Parsing
{
	public class SimpleParser : IParser
	{
		public IList<Dictionary<string, string>> ParseAnime (TextReader reader)
		{
			var result = new List<Dictionary<string, string>> ();

			var doc = new HtmlDocument ();
			doc.Load (reader);

			var nodes = doc.DocumentNode.SelectNodes ("//div[@class=\"animes\"]/table/tr");
			if (nodes == null) {
				return result;
			}

			foreach (var node in nodes) {
				var dataNodes = node.SelectNodes ("td");
				if (dataNodes != null) {
					var data = new Dictionary<string, string> ();

					data ["title"] = dataNodes [0].InnerText;
					data ["episodes"] = dataNodes [1].InnerText;

					result.Add (data);
				}
			}

			return result;
		}

		public IList<Dictionary<string, string>> ParseAnimePreview (TextReader reader)
		{
			throw new NotImplementedException ();
		}
	}
}

