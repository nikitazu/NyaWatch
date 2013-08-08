using System;
using System.Collections.Generic;
using System.IO;

namespace NyaWatch.Core.Parsing
{
	public interface IParser
	{
		IList<Dictionary<string, string>> ParseAnime(TextReader reader);
		IList<Dictionary<string, string>> ParseAnimePreview(TextReader reader);
	}
}

