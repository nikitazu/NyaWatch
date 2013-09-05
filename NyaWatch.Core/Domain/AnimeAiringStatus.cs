using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    public static class AnimeAiringStatus
    {
        public static string Calculate(int year, string airingStart, string airingEnd, DateTime today)
        {
            return 
                CalculateWithAiringDates(airingStart, airingEnd, today) ??
                CalculateWithYear(year, today) ??
                "Unknown";
        }

        public static string CalculateWithAiringDates(string airingStart, string airingEnd, DateTime today)
        {
            if (string.IsNullOrWhiteSpace(airingStart)) {
                return null;
            }

            var startDate = DateTime.Parse(airingStart).Date;
            if (startDate > today)
            {
                return "Not yet aired";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(airingEnd))
                {
#warning Movie support code should be here (Movie should be Aired not Airing)
                    return "Airing";
                }
                else
                {
                    var endDate = DateTime.Parse(airingEnd).Date;
                    return endDate <= today ? "Aired" : "Airing";
                }
            }
        }

        public static string CalculateWithYear(int year, DateTime today)
        {
            if (year > today.Year)
            {
                return "Not yet aired";
            }
            else if (year < today.Year)
            {
                return "Aired";
            }
            else
            {
                return null;
            }
        }
    }
}
