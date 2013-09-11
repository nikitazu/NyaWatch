using System;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
	[TestFixture]
	public class RussianDateConverterTests
	{
		[Test]
		public void TestConvertDate ()
		{
			Assert.AreEqual ("2010-08-25", RussianDateConverter.Convert ("25.08.2010"));
		}

		[Test]
		public void TestConvertNullOrEmptyDate()
		{
			Assert.IsNull (RussianDateConverter.Convert (null));
			Assert.IsNull (RussianDateConverter.Convert (string.Empty));
			Assert.IsNull (RussianDateConverter.Convert (" "));
		}

		[Test]
		public void TestFormatException()
		{
			Assert.Throws<FormatException> (() => RussianDateConverter.Convert ("flkjsdjls"));
		}
	}
}

