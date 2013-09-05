using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    public static class AnimeAiringStatus
    {
        public const string NotAired = "Not yet aired";
        public const string Aired = "Aired";
        public const string Airing = "Airing";
        public const string Unknown = "Unknown";

        public static string Calculate(IAnime anime, DateTime today)
        {
            return 
                CalculateWithAiringDates(anime, today) ??
                CalculateWithYear(anime, today) ??
                Unknown;
        }

        public static string CalculateWithAiringDates(IAnime anime, DateTime today)
        {
            if (!anime.AiringStart.HasValue) { return null; }

            if (anime.AiringStart.Value.Date > today)
            {
                return NotAired;
            }
            else
            {
                if (!anime.AiringEnd.HasValue)
                {
                    return anime.Type == "Movie" ? Aired : Airing;
                }
                else
                {
                    return anime.AiringEnd.Value.Date <= today ? Aired : Airing;
                }
            }
        }

        public static string CalculateWithYear(IAnime anime, DateTime today)
        {
            if (anime.Year == today.Year) { return null; }
            return anime.Year > today.Year ? NotAired : Aired;
        }
    }
}
