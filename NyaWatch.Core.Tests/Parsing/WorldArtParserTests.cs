using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
	[TestFixture()]
	public class WorldArtParserTests
	{
		IParser _parser;
		const string _parseAnimeFile = @"Parsing/WorldArtParser_TestParseAnime.html";

		[SetUp]
		public void Setup()
		{
			_parser = new WorldArtParser();
		}

		[TearDown]
		public void TearDown()
		{
			_parser = null;
		}

		[Test()]
		public void TestParseAnime ()
		{
			Dictionary<string, string> anime = null;

			using (var stream = File.OpenRead(_parseAnimeFile))
			using (var reader = new StreamReader(stream)) {
				anime = _parser.ParseAnime (reader);
			}
			File.WriteAllText ("/Users/nikitazu/test.txt", anime ["otherTitles"]);
			Assert.IsNotNull (anime, "Result should not be null");
			Assert.True (anime.ContainsKey ("otherTitles"), "otherTitles not found");
			Assert.AreEqual ("Slayers Excellent,Slayers: Lina-chan's Great Fashion Strategy,Slayers: The Fearful Future,Slayers: The Labyrinth,スレイヤーズえくせれんと",
			                 anime ["otherTitles"], "otherTitles wrong data");

		}
	}
}

