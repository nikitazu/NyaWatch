using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Data
{
    public static class DateSerializationExtension
    {
        public static string SerializeDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToString(DictionaryDataExtension.DateFormat) : string.Empty;
        }

        public static DateTime? DeserializeDate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return null; }
			return DateTime.ParseExact(value, DictionaryDataExtension.DateFormat, null);
        }
    }
}
