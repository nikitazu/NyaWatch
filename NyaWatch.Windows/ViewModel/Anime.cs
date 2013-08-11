using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

using NyaWatch.Core.ComponentModel;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.Windows.ViewModel
{
    public class Anime : Core.Domain.IAnime, INotifyPropertyChanged
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public int Episodes { get; set; }

        int _watched;

        public int Watched 
        {
            get { return _watched; }
            set { PropertyChanged.ChangeAndNotify(this, ref _watched, value, () => Watched); }
        }

        public Anime IncrementWatched()
        {
            Watched += 1;
            return this;
        }

        public bool CanIncrementWatched()
        {
            return Episodes > 0 && Watched < Episodes;
        }

        public Anime DecrementWatched()
        {
            Watched -= 1;
            return this;
        }

        public bool CanDecrementWatched()
        {
            return Watched > 0;
        }

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
            get { return MaybeActiveTextBrush(TorrentsCount > 0); }
        }

        public string Status { get; set; }

        public Brush StatusColor
        {
            get { return MaybeActiveTextBrush(Status == "Airing"); }
        }

        public string ImagePath { get; set; }

        public string ActualImagePath
        {
            get { return ImagePath ?? "pack://application:,,,/Resources/icon_256x256.png"; }
        }

        bool _pinned = false;
        public bool Pinned
        {
            get { return _pinned; }
            set { PropertyChanged.ChangeAndNotify(this, ref _pinned, value, () => Pinned); }
        }

        private Brush MaybeActiveTextBrush(bool isActive)
        {
            return isActive ? SystemColors.HotTrackBrush : SystemColors.GrayTextBrush;
        }

        public Root Root { get; set; }
        public Anime WithRoot(Root root)
        {
            Root = root;
            return this;
        }

        public Anime(string title, string type, int episodes, int torrents, string status)
        {
            Title = title;
            Type = type;
            Episodes = episodes;
            TorrentsCount = torrents;
            Status = status;
        }

        public Anime()
        {
            // empty
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
