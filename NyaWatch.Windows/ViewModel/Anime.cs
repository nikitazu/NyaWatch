﻿using System;
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

        string _status = cd.AnimeAiringStatus.Unknown;
        public string Status 
        {
            get { return _status; }
            set { PropertyChanged.ChangeAndNotify(this, ref _status, value, () => Status); }
        }

        public Brush StatusColor
        {
            get { return MaybeActiveTextBrush(Status == "Airing"); }
        }

        string _imagePath = "pack://application:,,,/Resources/icon_256x256.png";
        public string ImagePath 
        {
            get { return _imagePath; }
            set 
            {
                if (System.IO.File.Exists (value))
                {
                    PropertyChanged.ChangeAndNotify (this, ref _imagePath, value, () => ImagePath);
                }
            }
        }

        public string ImageUrl { get; set; }

        bool _pinned = false;
        public bool Pinned
        {
            get { return _pinned; }
            set { PropertyChanged.ChangeAndNotify(this, ref _pinned, value, () => Pinned); }
        }

        public Brush PinnedColor
        {
            get { return MaybeActiveTextBrush(Pinned); }
        }

        private Brush MaybeActiveTextBrush(bool isActive)
        {
            return isActive ? SystemColors.HotTrackBrush : SystemColors.GrayTextBrush;
        }

        DateTime? _airingStart;
        public DateTime? AiringStart 
        {
            get { return _airingStart; }
            set
            {
                PropertyChanged.ChangeAndNotify (this, ref _airingStart, value, () => AiringStart);
                Status = cd.AnimeAiringStatus.Calculate (this, DateTime.Today);
            }
        }

        DateTime? _airingEnd;
		public DateTime? AiringEnd 
        {
            get { return _airingEnd; }
            set
            {
                PropertyChanged.ChangeAndNotify (this, ref _airingEnd, value, () => AiringEnd);
                Status = cd.AnimeAiringStatus.Calculate (this, DateTime.Today);
            }
        }

		public int Year { get; set; }

        public Root Root { get; set; }
        public Anime WithRoot(Root root)
        {
            Root = root;
            return this;
        }

        public Anime(string title, string type, int episodes, int torrents, string airingStart, string airingEnd, int year)
        {
            Title = title;
            Type = type;
            Episodes = episodes;
            TorrentsCount = torrents;
            Year = year;

            try
            {
                AiringStart = DateTime.Parse(airingStart);
            } catch (FormatException)
            {
                AiringStart = null;
            }

            try
            {
                AiringEnd = DateTime.Parse(airingEnd);
            }
            catch (FormatException)
            {
                AiringEnd = null;
            }

            Status = cd.AnimeAiringStatus.Calculate(this, DateTime.Today);
        }

        public Anime()
        {
            // empty
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
