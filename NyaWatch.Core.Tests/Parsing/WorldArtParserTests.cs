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
		const string _parseAnimeUrl = @"http://www.world-art.ru/animation/animation.php?id=203";

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

			anime = _parser.ParseAnimeFromFile (_parseAnimeFile);
			//anime = _parser.ParseAnimeFromWeb (_parseAnimeUrl);

			Assert.IsNotNull (anime, "Result should not be null");

			Assert.True (anime.ContainsKey ("otherTitles"), "otherTitles not found");
			Assert.True (anime.ContainsKey ("country"), "country not found");
			Assert.True (anime.ContainsKey ("title"), "title not found");
			Assert.True (anime.ContainsKey ("year"), "year not found");
			Assert.True (anime.ContainsKey ("type"), "type not found");
			Assert.True (anime.ContainsKey ("episodes"), "episodes not found");
			Assert.True (anime.ContainsKey ("airingStart"), "airingStart not found");
			Assert.True (anime.ContainsKey ("airingEnd"), "airingEnd not found");
			Assert.True (anime.ContainsKey ("imageUrl"), "imageUrl not found");
			Assert.True (anime.ContainsKey ("posterUrl"), "posterUrl not found");

			Assert.AreEqual ("Slayers Excellent,Slayers: Lina-chan's Great Fashion Strategy,Slayers: The Fearful Future,Slayers: The Labyrinth,スレイヤーズえくせれんと",
			                 anime ["otherTitles"], "otherTitles wrong data");

			Assert.AreEqual ("Япония", anime ["country"], "country wrong data");
			Assert.AreEqual ("Превосходные Рубаки", anime ["title"], "title wrong data");
			Assert.AreEqual ("1998", anime ["year"], "year wrong data");
			Assert.AreEqual ("OVA", anime ["type"], "type wrong data");
			Assert.AreEqual ("3", anime ["episodes"], "episodes wrong data");
			Assert.AreEqual ("25.10.1998", anime ["airingStart"], "airingStart wrong data");
			Assert.AreEqual ("25.03.1999", anime ["airingEnd"], "airingEnd wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/img/1000/203/1.jpg", anime ["imageUrl"], "imageUrl wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/animation_poster.php?id=203", anime ["posterUrl"], "posterUrl wrong data");
		}
	}
}

