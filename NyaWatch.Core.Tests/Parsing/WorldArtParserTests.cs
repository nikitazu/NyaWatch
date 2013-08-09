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

			Assert.IsNotNull (anime, "Result should not be null");
		}
	}
}

