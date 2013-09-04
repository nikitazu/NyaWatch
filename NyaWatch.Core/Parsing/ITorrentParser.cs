using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NyaWatch.Core
{
	public interface ITorrentParser
	{
		List<Dictionary<string, string>> ParseTorrentsFromString(string xml);
		List<Dictionary<string, string>> ParseTorrentsFromFile(string path);
		List<Dictionary<string, string>> ParseTorrentsFromWeb(string url);
	}

	public abstract class TorrentParser : ITorrentParser
	{
		public List<Dictionary<string, string>> ParseTorrentsFromString(string xml)
		{
			return ParseTorrentsFromXml (XDocument.Parse (xml));

		}

		public List<Dictionary<string, string>> ParseTorrentsFromFile(string path)
		{
			return ParseTorrentsFromXml (XDocument.Load (path));
		}

		public List<Dictionary<string, string>> ParseTorrentsFromWeb(string url)
		{
			return ParseTorrentsFromXml (XDocument.Load (url));
		}

		protected abstract List<Dictionary<string, string>> ParseTorrentsFromXml (XDocument xml);
	}
}

