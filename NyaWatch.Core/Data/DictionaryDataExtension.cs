using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDic = System.Collections.Generic.IDictionary<string, string>;

namespace NyaWatch.Core.Data
{
    public static class DictionaryDataExtension
    {
        public static string RequireString (this IDic item, string key)
        {
            if (item == null) { throw new ArgumentNullException ("item"); }
            return item[key];
        }

        public static int RequireInt (this IDic item, string key)
        {
            return int.Parse (item.RequireString (key));
        }

        public static bool RequireBool (this IDic item, string key)
        {
            return bool.Parse (item.RequireString (key));
        }

        public const string DateFormat = "yyyy-MM-dd";
        public static DateTime RequireDate (this IDic item, string key)
        {
            return DateTime.ParseExact (item.RequireString (key), DateFormat, null);
        }


        public static string OptionalString (this IDic item, string key)
        {
            if (item == null) { throw new ArgumentNullException ("item"); }
            try
            {
                return item[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static int? OptionalInt (this IDic item, string key)
        {
            var data = item.OptionalString (key);
            return data == null ? null : new Nullable<int> (int.Parse (data));
        }

        public static bool? OptionalBool (this IDic item, string key)
        {
            var data = item.OptionalString (key);
            return data == null ? null : new Nullable<bool> (bool.Parse (data));
        }

        public static DateTime? OptionalDate (this IDic item, string key)
        {
            return item.OptionalString (key).DeserializeDate ();
        }
    }
}
