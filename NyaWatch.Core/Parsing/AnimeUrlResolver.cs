using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Parsing
{
    public class AnimeUrlResolver
    {
        public IParser CreateParserFor(string searchData)
        {
            if (searchData.StartsWith("http://www.world-art.ru")) {
                return new WorldArtParser();
            }

            return null;
        }
    }
}
