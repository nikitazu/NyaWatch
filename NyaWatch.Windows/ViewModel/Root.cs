using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

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

            Core.Domain.Anime.Put(Core.Domain.Categories.PlanToWatch, a1);
            Core.Domain.Anime.Put(Core.Domain.Categories.PlanToWatch, a2);
            Core.Domain.Anime.Put(Core.Domain.Categories.Watching, a3);
            Core.Domain.Anime.Put(Core.Domain.Categories.Watching, a4);
            Core.Domain.Anime.Put(Core.Domain.Categories.Completed, a5);
            Core.Domain.Anime.Put(Core.Domain.Categories.Completed, a6);
            Core.Domain.Anime.Put(Core.Domain.Categories.Completed, a7);
            Core.Domain.Anime.Put(Core.Domain.Categories.OnHold, a8);
            Core.Domain.Anime.Put(Core.Domain.Categories.Dropped, a9);

            Animes.AddRange(Core.Domain.Anime.Find<Anime>(Core.Domain.Categories.Watching));
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
