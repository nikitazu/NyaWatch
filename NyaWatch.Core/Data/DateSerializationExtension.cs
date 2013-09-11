using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Data
{
    public static class DateSerializationExtension
    {
        const string Format = "yyyy-MM-dd";

        public static string SerializeDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToString(Format) : string.Empty;
        }

        public static DateTime? DeserializeDate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) { return null; }
            return DateTime.ParseExact(value, Format, null);
        }
    }
}
