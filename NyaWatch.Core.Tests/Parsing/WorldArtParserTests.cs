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

		const string _parseAnimeMovieFile = @"Parsing/WorldArtParser_TestParseAnime3.html";
		const string _parseAnimeMovieUrl = @"http://www.world-art.ru/animation/animation.php?id=147";

		const string _parseAnimeRailgunSFile = @"Parsing/WorldArtParser_TestParseAnime_RailgunS.html";
		const string _parseAnimeRailgunSUrl = @"http://www.world-art.ru/animation/animation.php?id=1895";

		const string _parseAnimeRailgunWithSpecialsFile= @"Parsing/WorldArtParser_TestParseAnime_RailgunWithSpecials.html";
		const string _parseAnimeRailgunWithSpecialsUrl = @"http://www.world-art.ru/animation/animation.php?id=7354";


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
			Assert.AreEqual ("1998-10-25", anime ["airingStart"], "airingStart wrong data");
			Assert.AreEqual ("1999-03-25", anime ["airingEnd"], "airingEnd wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/img/1000/203/1.jpg", anime ["imageUrl"], "imageUrl wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/animation_poster.php?id=203", anime ["posterUrl"], "posterUrl wrong data");
		}

		[Test]
		public void TestParseAnimeMovie()
		{
			Dictionary<string, string> anime = null;

			anime = _parser.ParseAnimeFromFile (_parseAnimeMovieFile);
			//anime = _parser.ParseAnimeFromWeb (_parseAnimeMovieUrl);

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

			Assert.AreEqual ("Shin Evangelion Gekijouban:||,シン・エヴァンゲリオン劇場版：||",
			                 anime ["otherTitles"], "otherTitles wrong data");

			Assert.AreEqual ("Япония", anime ["country"], "country wrong data");
			Assert.AreEqual ("Евангелион по-новому (фильм четвёртый)", anime ["title"], "title wrong data");
			Assert.AreEqual ("2015", anime ["year"], "year wrong data");
			Assert.AreEqual ("Movie", anime ["type"], "type wrong data");
			Assert.AreEqual ("1", anime ["episodes"], "episodes wrong data");
			Assert.AreEqual ("", anime ["airingStart"], "airingStart wrong data");
			Assert.AreEqual ("", anime ["airingEnd"], "airingEnd wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/img/1000/147/1.jpg", anime ["imageUrl"], "imageUrl wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/animation_poster.php?id=147", anime ["posterUrl"], "posterUrl wrong data");
		}

		[Test]
		public void TestParseAnimeRailgunS()
		{
			// [ ] symbols in name
			Dictionary<string, string> anime = null;

			anime = _parser.ParseAnimeFromFile (_parseAnimeRailgunSFile);
			//anime = _parser.ParseAnimeFromWeb (_parseAnimeRailgunSUrl);

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

			Assert.AreEqual ("Toaru Kagaku no Railgun S,とある科学の超電磁砲Ｓ",
			                 anime ["otherTitles"], "otherTitles wrong data");

			Assert.AreEqual ("Япония", anime ["country"], "country wrong data");
			Assert.AreEqual ("Некий научный Рейлган [ТВ-2]", anime ["title"], "title wrong data");
			Assert.AreEqual ("2013", anime ["year"], "year wrong data");
			Assert.AreEqual ("TV", anime ["type"], "type wrong data");
			Assert.AreEqual ("24", anime ["episodes"], "episodes wrong data");
			Assert.AreEqual ("2013-04-12", anime ["airingStart"], "airingStart wrong data");
			Assert.AreEqual ("2013-09-27", anime ["airingEnd"], "airingEnd wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/img/2000/1895/1.jpg", anime ["imageUrl"], "imageUrl wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/animation_poster.php?id=1895", anime ["posterUrl"], "posterUrl wrong data");
		}

		[Test]
		public void TestParseAnimeRailgunWithSpecials()
		{
			// has specials in episodes definition
			Dictionary<string, string> anime = null;

			anime = _parser.ParseAnimeFromFile (_parseAnimeRailgunWithSpecialsFile);
			//anime = _parser.ParseAnimeFromWeb (_parseAnimeRailgunWithSpecialsUrl);

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

			Assert.AreEqual ("A Certain Scientific Railgun,Toaru Kagaku no Railgun,To Aru Kagaku no Railgun,とある科学の超電磁砲,とある科学の超電磁砲（レールガン）",
			                 anime ["otherTitles"], "otherTitles wrong data");

			Assert.AreEqual ("Япония", anime ["country"], "country wrong data");
			Assert.AreEqual ("Некий научный Рейлган [ТВ-1]", anime ["title"], "title wrong data");
			Assert.AreEqual ("2009", anime ["year"], "year wrong data");
			Assert.AreEqual ("TV", anime ["type"], "type wrong data");
			Assert.AreEqual ("24", anime ["episodes"], "episodes wrong data");
			Assert.AreEqual ("2009-10-03", anime ["airingStart"], "airingStart wrong data");
			Assert.AreEqual ("2010-03-20", anime ["airingEnd"], "airingEnd wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/img/8000/7354/1.jpg", anime ["imageUrl"], "imageUrl wrong data");
			Assert.AreEqual ("http://www.world-art.ru/animation/animation_poster.php?id=7354", anime ["posterUrl"], "posterUrl wrong data");
		}
	}
}

