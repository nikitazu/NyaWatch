using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
	[TestFixture()]
	public class NyaaTorrentParserTests
	{
		ITorrentParser _parser;

		[SetUp]
		public void Setup()
		{
			_parser = new NyaaTorrentParser ();
		}

		[TearDown]
		public void TearDown()
		{
			_parser = null;
		}

		[Test]
		public void TestParseTorrentsFromFile ()
		{
			var torrents = _parser.ParseTorrentsFromFile ("Parsing/NyaaTorrentParser1.xml");

			Assert.NotNull (torrents, "Torrents collection should be created");
			Assert.AreEqual (3, torrents.Count, "3 torrents should be found");

			AssertKey (torrents, 0, "title", "Tengen Toppa Gurren Lagann 01-27 FULL BDRip X.264 FLAC 2.0 Sub.ENG - ETB (v3)");
			AssertKey (torrents, 1, "title", "[A-GX] Shingeki no Kyojin - 21v2 [3B024B18].mkv");
			AssertKey (torrents, 2, "title", "[BKT] Shingeki no Kyojin 20 (720p_x264+AAC_SUB ITA)");

			AssertKey (torrents, 0, "category", Domain.TorrentCategory.English.ToString());
			AssertKey (torrents, 1, "category", Domain.TorrentCategory.NonEnglish.ToString());
			AssertKey (torrents, 2, "category", Domain.TorrentCategory.NonEnglish.ToString());
			
			AssertKey (torrents, 0, "seeders", "3");
			AssertKey (torrents, 1, "seeders", "0");
			AssertKey (torrents, 2, "seeders", "17");

			AssertKey (torrents, 0, "leechers", "34");
			AssertKey (torrents, 1, "leechers", "0");
			AssertKey (torrents, 2, "leechers", "4");

			AssertKey (torrents, 0, "downloads", "579");
			AssertKey (torrents, 1, "downloads", "294");
			AssertKey (torrents, 2, "downloads", "305");

			AssertKey (torrents, 0, "size", "33.95 GiB");
			AssertKey (torrents, 1, "size", "690.6 MiB");
			AssertKey (torrents, 2, "size", "454.5 MiB");
		}

		void AssertKey(List<Dictionary<string, string>> torrents, int index, string key, string value)
		{
			var torrent = torrents [index];
			Assert.IsTrue (torrent.ContainsKey (key), "Key {0} not found in torrent {1}", key, index);
			Assert.AreEqual (value, torrent [key], "Wrong {0} for torrent {1}", key, index);
		}
	}
}

