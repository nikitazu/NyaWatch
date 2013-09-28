using System;
using System.Text.RegularExpressions;

namespace NyaWatch.Core.Parsing
{
	public static class NameCleaner
	{
		readonly static Regex _shittyCodes = 
			new Regex (@"\[[a-z|0-9]*[a-z]+[a-z|0-9]*\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		readonly static string[] _complexFansubers = {
			"[UTW-Mazui]",
			"[Sena-Raws]",
			"[Zero-Raws]"
		};
		
		readonly static Regex _badKeywords = 
			new Regex (@"(1920x1080|1280x720|1080p|720p|x264|AAC|mawen1250|Hi10P)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		readonly static string[] _complexBadKeywords = {
			"320K+",
			"HDTV-720",
			"BD AVC-yuv420p10 FLAC",
			"x264-Hi10P",
			"MX x264 AAC",
			"AT-X HD!"
		};

		readonly static Regex _unnesessarySpaces = new Regex (@"\s\s+", RegexOptions.Compiled);

		readonly static string[] _lastTouches = {
			// empty brackets
			"[]",
			"()",
			"{}",
			"\"\"",
			"''",
			// empty brackets left by removing unnesesary spaces
			"[ ]",
			"( )",
			"{ }",
			"\" \"",
			"' '"
		};

		public static string Clean(string name)
		{
			var temp = _unnesessarySpaces.Replace (name, " ");

			foreach (var fansuber in _complexFansubers) {
				temp = temp.Replace (fansuber, string.Empty);
			}

			foreach (var badKeyword in _complexBadKeywords) {
				temp = temp.Replace (badKeyword, string.Empty);
			}

			temp = _shittyCodes.Replace (temp, string.Empty);
			temp = _badKeywords.Replace (temp, string.Empty);
			temp = _unnesessarySpaces.Replace (temp, " ");

			foreach (var touch in _lastTouches) {
				temp = temp.Replace (touch, string.Empty);
			}

			return temp.Trim();
		}
	}
}

