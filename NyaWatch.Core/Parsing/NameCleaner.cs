using System;
using System.Text.RegularExpressions;

namespace NyaWatch.Core.Parsing
{
	public static class NameCleaner
	{
		readonly static Regex _shittyCodes = 
			new Regex (@"\[[a-z|0-9]*[a-z]+[a-z|0-9]*\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		readonly static string[] _complexFansubers = {
			"[UTW-Mazui]"
		};
		
		readonly static Regex _badKeywords = 
			new Regex (@"(1920x1080|1280x720|1080p|720p)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		readonly static string[] _complexBadKeywords = {
			"320K+",
			"HDTV-720"
		};

		readonly static string[] _lastTouches = {
			"[]",
			"()",
			"{}",
			"\"\"",
			"''"
		};

		public static string Clean(string name)
		{
			var temp = _shittyCodes.Replace (name, string.Empty);
			temp = _badKeywords.Replace (temp, string.Empty);

			foreach (var fansuber in _complexFansubers) {
				temp = temp.Replace (fansuber, string.Empty);
			}

			foreach (var badKeyword in _complexBadKeywords) {
				temp = temp.Replace (badKeyword, string.Empty);
			}

			foreach (var touch in _lastTouches) {
				temp = temp.Replace (touch, string.Empty);
			}

			return temp.Trim();
		}
	}
}

