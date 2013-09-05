using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NyaWatch.Core.Parsing.Tests
{
    [TestFixture]
    public class AnimeUrlResolverTests
    {
        const string WorldArtDirectLink = "http://www.world-art.ru/animation/animation.php?id=395";
        const string NormalSearch = "Neon Genesis Evangelion";

        [Test]
        public void TestResolve()
        {
            var resolver = new AnimeUrlResolver();
            var parser = resolver.CreateParserFor(WorldArtDirectLink);
            Assert.IsNotNull(parser, "WorldArt parser should not be null for direct link");
            Assert.AreEqual(typeof(WorldArtParser), parser.GetType(), "WorldArt parser type should be correct");
        }

        [Test]
        public void TestNotResolve()
        {
            var resolver = new AnimeUrlResolver();
            var parser = resolver.CreateParserFor(NormalSearch);
            Assert.IsNull(parser, "Parser should be null for normal search");
        }
    }
}
