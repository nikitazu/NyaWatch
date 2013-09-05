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

        IAnime _notAired;
        IAnime _airing1;
        IAnime _airing2;
        IAnime _aired;
        
        IAnime _unknown1;
        IAnime _unknown2;

        IAnime _notAiredByYear;
        IAnime _airedByYear;

        [SetUp]
        public void Setup()
        {
            _notAired = new AnimeDummy
            {
                AiringStart = new DateTime(2011, 1, 1),
                AiringEnd = new DateTime(2012, 1, 1)
            };
            _airing1 = new AnimeDummy
            {
                AiringStart = new DateTime(2009, 12, 1),
                AiringEnd = new DateTime(2011, 10, 1)
            };
            _airing2 = new AnimeDummy
            {
                AiringStart = new DateTime(2009, 12, 1),
                AiringEnd = null
            };
            _aired = new AnimeDummy
            {
                AiringStart = new DateTime(2007, 1, 1),
                AiringEnd = new DateTime(2008, 1, 1)
            };

            _unknown1 = new AnimeDummy();
            _unknown2 = new AnimeDummy
            {
                Year = Today.Year
            };

            _notAiredByYear = new AnimeDummy
            {
                Year = 2100
            };
            _airedByYear = new AnimeDummy
            {
                Year = 1995
            };
        }

        [TearDown]
        public void TearDown()
        {
            _notAired= null;
            _airing1= null;
            _airing2= null;
            _aired= null;

            _unknown1 = null;
            _unknown2 = null;

            _notAiredByYear= null;
            _airedByYear= null;
        }

        [Test]
        public void TestCalculateNotYetAired()
        {
            Assert.AreEqual(AnimeAiringStatus.NotAired, AnimeAiringStatus.CalculateWithAiringDates(_notAired, Today));
        }

        [Test]
        public void TestCalculateAiring()
        {
            Assert.AreEqual(AnimeAiringStatus.Airing, AnimeAiringStatus.CalculateWithAiringDates(_airing1, Today));
            Assert.AreEqual(AnimeAiringStatus.Airing, AnimeAiringStatus.CalculateWithAiringDates(_airing2, Today));
        }

        [Test]
        public void TestCalculateAired()
        {
            Assert.AreEqual(AnimeAiringStatus.Aired, AnimeAiringStatus.CalculateWithAiringDates(_aired, Today));
        }

        [Test]
        public void TestCalculateUnknown()
        {
            Assert.IsNull(AnimeAiringStatus.CalculateWithAiringDates(_unknown1, Today));
            Assert.IsNull(AnimeAiringStatus.CalculateWithYear(_unknown2, Today));
            Assert.AreEqual(AnimeAiringStatus.Unknown, AnimeAiringStatus.Calculate(_unknown2, Today));
        }

        [Test]
        public void CalculateWithYear()
        {
            Assert.AreEqual(AnimeAiringStatus.NotAired, AnimeAiringStatus.CalculateWithYear(_notAiredByYear, Today));
            Assert.AreEqual(AnimeAiringStatus.Aired, AnimeAiringStatus.CalculateWithYear(_airedByYear, Today));
        }
    }
}
