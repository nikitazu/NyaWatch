using System;

namespace NyaWatch.Core.Parsing
{
	public static class RussianDateConverter
	{
		public static string Convert(string date)
		{
			if (string.IsNullOrWhiteSpace (date)) { return null; }
			return DateTime.ParseExact (date, "dd.MM.yyyy", null).ToString ("yyyy-MM-dd");
		}
	}
}

