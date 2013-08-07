﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.Windows.ViewModel
{
    public class Root : INotifyPropertyChanged
    {
        List<Anime> _animes;

        public List<Anime> Animes 
        {
            get { return _animes; }
            set
            {
                if (_animes != value)
                {
                    _animes = value;
                    OnChanged("Animes");
                }
            }
        }
        
        
        public Anime SelectedAnime { get; set; }
        public cd.Categories SelectedCategory { get; set; }

        public Root()
        {
            Animes = new List<Anime>();

            var a1 = new Anime("Slayers: Excellent", "OVA", 3, 0, "Aired");
            var a2 = new Anime("Asura", "Movie", 1, 1, "Not yet aired");
            var a3 = new Anime("Slayers", "TV", 26, 4, "Airing");
            var a4 = new Anime("Neon Genesis Evangelion", "TV", 26, 0, "Aired");
            var a5 = new Anime("Slayers: Next", "TV", 26, 4, "Airing");
            var a6 = new Anime("Slayers: Try", "TV", 26, 4, "Airing");
            var a7 = new Anime("Slayers: Perfect", "OVA", 3, 4, "Airing");
            var a8 = new Anime("Slayers: Gourgeous", "OVA", 3, 4, "Airing");
            var a9 = new Anime("Slayers: The motion picture", "Movie", 1, 4, "Airing");

            cd.Anime.Put(cd.Categories.PlanToWatch, a1);
            cd.Anime.Put(cd.Categories.PlanToWatch, a2);
            cd.Anime.Put(cd.Categories.Watching, a3);
            cd.Anime.Put(cd.Categories.Watching, a4);
            cd.Anime.Put(cd.Categories.Completed, a5);
            cd.Anime.Put(cd.Categories.Completed, a6);
            cd.Anime.Put(cd.Categories.Completed, a7);
            cd.Anime.Put(cd.Categories.OnHold, a8);
            cd.Anime.Put(cd.Categories.Dropped, a9);

            Animes.AddRange(cd.Anime.Find<Anime>(cd.Categories.Watching));
            SelectedCategory = cd.Categories.Watching;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
