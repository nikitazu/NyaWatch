using System;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
	[TestFixture]
	public class NameCleanerTests
	{
		readonly string[] _dirtyNames = {
			"[Commie] name",
			"[DeadFish] name",
			"[UTW-Mazui] name",
			"[EightBitMKV] name",
			"[Hatsuyuki] name",
			"[Sena-Raws] name",
			"[Zero-Raws] name",
			"name 720p",
			"name 1080p",
			"name 1280x720",
			"name 1920x1080",
			"name HDTV-720",
			"name 320K+",
			"name [1280x720]",
			"name (1920x1080)",
			"name {HDTV-720}",
			"name \"320K+\"",
			"[UTW-Mazui]name[720p][CD07F005]",
			"name x264-Hi10P AAC",
			"name BD  AVC-yuv420p10 FLAC",
			"name BD AVC-yuv420p10 FLAC",
			"name mawen1250",
			"name AT-X HD!  x264 AAC",
			"name MX  x264 AAC",
			"name AT-X HD!  x264 Hi10P AAC",
			"name x264 AAC",
			"name AT-X HD!  Hi10P",
		};

		readonly string[] _niceNames = {
			"name - 1",
			"Commie",
			"[name is][01]"
		};

		readonly string[] _unnecesarySpaces = {
			"(     )",
			"(    )",
			"(  )",
			"( )",
			"()",
		};

		[Test]
		public void TestDirtyNames()
		{
			foreach (var dirty in _dirtyNames) {
				var clean = NameCleaner.Clean (dirty);
				Assert.AreEqual ("name", clean, "Name \"{0}\" was not cleaned well, \"{1}\" was left", dirty, clean);
			}
		}

		[Test]
		public void TestNiceNames()
		{
			foreach (var nice in _niceNames) {
				var clean = NameCleaner.Clean (nice);
				Assert.AreEqual (nice, clean, "Nice name should be the same after cleaning");
			}
		}

		[Test]
		public void TestUnnecesarySpaces()
		{
			foreach (var space in _unnecesarySpaces) {
				var clean = NameCleaner.Clean (space);
				Assert.AreEqual (string.Empty, clean, "Unnecessary spaces should be removed");
			}
		}
	}
}

