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
			"[UTW-Mazui]name[720p][CD07F005]"
		};

		readonly string[] _niceNames = {
			"name - 1",
			"Commie",
			"[name is][01]"
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
	}
}

