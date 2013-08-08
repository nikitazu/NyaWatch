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

		public IList<Dictionary<string, string>> ParseAnime (TextReader reader)
		{
			var doc = new HtmlDocument ();
			doc.Load (reader);

			var table = doc.DocumentNode.SelectSingleNode ("//html/body//table[6]");
			if (table == null) {
				return null;
			}

			//var x = table.OuterHtml ();
			//throw new Exception (x);

			var rows = table.SelectNodes ("tr");
			if (rows == null) {
				return null;
			}

			var results = new List<Dictionary<string, string>> ();

			// data starts at row 5 and last row is useless
			var dataRows = rows.Skip (5).Take (rows.Count () - 5);
			foreach (var row in rows) {
				var result = new Dictionary<string, string> ();
				throw new Exception (row.OuterHtml);
				results.Add (result);
			}

			return results;
		}

		public IList<Dictionary<string, string>> ParseAnimePreview (TextReader reader)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

