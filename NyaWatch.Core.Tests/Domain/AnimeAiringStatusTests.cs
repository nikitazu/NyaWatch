using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NyaWatch.Core.Domain.Tests.Domain
{
    [TestFixture]
    public class AnimeAiringStatusTests
    {
        static readonly DateTime Today = new DateTime(2010, 1, 1);

        [Test]
        public void TestCalculateNotYetAired()
        {
            Assert.AreEqual("Not yet aired", AnimeAiringStatus.CalculateWithAiringDates("01.01.2011", "01.01.2012", Today));
        }

        [Test]
        public void TestCalculateAiring()
        {
            Assert.AreEqual("Airing", AnimeAiringStatus.CalculateWithAiringDates("01.12.2009", "01.10.2011", Today));
            Assert.AreEqual("Airing", AnimeAiringStatus.CalculateWithAiringDates("01.12.2009", "", Today));
        }

        [Test]
        public void TestCalculateAired()
        {
            Assert.AreEqual("Aired", AnimeAiringStatus.CalculateWithAiringDates("01.01.2007", "01.01.2008", Today));
        }

        [Test]
        public void TestCalculateUnknown()
        {
            Assert.IsNull(AnimeAiringStatus.CalculateWithAiringDates("", "01.01.2008", Today));
        }

        [Test]
        public void CalculateWithYear()
        {
            Assert.AreEqual("Not yet aired", AnimeAiringStatus.CalculateWithYear(2100, Today));
            Assert.AreEqual("Aired", AnimeAiringStatus.CalculateWithYear(1995, Today));
            Assert.IsNull(AnimeAiringStatus.CalculateWithYear(Today.Year, Today));
        }

        [Test]
        public void CalculateUnknown()
        {
            Assert.AreEqual("Unknown", AnimeAiringStatus.Calculate(Today.Year, "", "", Today));
        }
    }
}
