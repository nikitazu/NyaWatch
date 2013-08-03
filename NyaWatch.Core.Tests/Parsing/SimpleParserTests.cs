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
				<tr><td>Slayers</td><td>26</td></tr>
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
			using (var reader = new StringReader(_html)) {
				var animes = _parser.ParseAnime (reader);
				Assert.IsNotNull (animes, "Should be some data");
				Assert.AreEqual (2, animes.Count (), "Should find 2 animes");

				var nge = animes [0];
				var slayers = animes [1];

				Assert.IsTrue (nge.ContainsKey ("title"), "Title for nge not found");
				Assert.AreEqual ("NGE", nge ["title"], "Title for nge should be NGE");
				Assert.IsTrue (nge.ContainsKey ("episodes"), "Episodes for nge not found");
				Assert.AreEqual ("26", nge ["episodes"], "Episodes for nge should be 26");

				Assert.IsTrue (slayers.ContainsKey ("title"), "Title for nge not found");
				Assert.AreEqual ("Slayers", slayers ["title"], "Title for nge should be Slayers");
				Assert.IsTrue (slayers.ContainsKey ("episodes"), "Episodes for nge not found");
				Assert.AreEqual ("26", slayers ["episodes"], "Episodes for nge should be 26");
			}
		}
	}
}

