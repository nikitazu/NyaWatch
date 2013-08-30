using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
	[TestFixture()]
	public class SimpleParserTests
	{
		IParser _parser;
		string _html = @"
<html>
	<head><title>Simple parser example</title></head>
	<body>
		<div>Some not needed stuff</div>
		<div class=""animes"">
			<table>
				<tr><th>Title</th><th>Episodes</th></tr>
				<tr><td>NGE</td><td>26</td></tr>
			</table>
		</div>
		<div>Other useless stuff</div>
	</body>
</html>
";

		[SetUp]
		public void Setup()
		{
			_parser = new SimpleParser();
		}

		[TearDown]
		public void TearDown()
		{
			_parser = null;
		}

		[Test()]
		public void TestParseAnime ()
		{
			var anime = _parser.ParseAnimeFromString (_html);
			Assert.IsNotNull (anime, "Should be some data");
			Assert.IsTrue (anime.ContainsKey ("title"), "Title for nge not found");
			Assert.AreEqual ("NGE", anime ["title"], "Title for nge should be NGE");
			Assert.IsTrue (anime.ContainsKey ("episodes"), "Episodes for nge not found");
			Assert.AreEqual ("26", anime ["episodes"], "Episodes for nge should be 26");
		}
	}
}

