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

        public Brush TypeColor
        {
            get
            {
                switch (Type)
                {
                    case "TV":
                        return Brushes.Brown;
                    case "OVA":
                        return Brushes.Orange;
                    case "Movie":
                        return Brushes.Magenta;
                    default:
                        return Brushes.Black;
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

        public Brush TorrentsColor
        {
            get
            {
                return TorrentsCount > 0 ? Brushes.Blue : Brushes.LightGray;
            }
        }

        public string Status { get; set; }

        public Brush StatusColor
        {
            get
            {
                return Status == "Airing" ? Brushes.Blue : Brushes.LightGray;
            }
        }

        public string ImagePath { get; set; }

        public string ActualImagePath
        {
            get
            {
                return ImagePath ?? "pack://application:,,,/Resources/icon_256x256.png";
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
