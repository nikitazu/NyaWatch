using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace NyaWatch.Windows.ViewModel
{
    public class Anime
    {
        public string Title { get; set; }

        public int Episodes { get; set; }

        public int Watched { get; set; }

        public string Type { get; set; }

        public Color TypeColor
        {
            get
            {
                switch (Type)
                {
                    case "TV":
                        return Colors.Brown;
                    case "OVA":
                        return Colors.Orange;
                    case "Movie":
                        return Colors.Magenta;
                    default:
                        return Colors.Black;
                }
            }
        }

        public int TorrentsCount { get; set; }

        public string TorrentsMessage
        {
            get
            {
                switch (TorrentsCount)
                {
                    case 0:
                        return "No torrents found";
                    case 1:
                        return "One torrent found";
                    default:
                        return string.Format("{0} torrents found", TorrentsCount);
                }
            }
        }

        public Color TorrentsColor
        {
            get
            {
                return TorrentsCount > 0 ? Colors.Blue : Colors.LightGray;
            }
        }

        public string Status { get; set; }

        public Color StatusColor
        {
            get
            {
                return Status == "Airing" ? Colors.Blue : Colors.LightGray;
            }
        }

        public Anime(string title, string type, int episodes, int torrents, string status)
        {
            Title = title;
            Type = type;
            Episodes = episodes;
            TorrentsCount = torrents;
            Status = status;
        }
    }
}
