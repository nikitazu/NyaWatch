using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Windows.ViewModel
{
    public class Root
    {
        public List<Anime> Animes { get; private set; }
        public Anime SelectedAnime { get; set; }

        public Root()
        {
            Animes = new List<Anime>();

            var a1 = new Anime("Slayers: Excellent", "OVA", 20, 0, "Aired");
            var a2 = new Anime("Asura", "Movie", 25, 1, "Not yet aired");
            var a3 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a4 = new Anime("Neon Genesis Evangelion", "TV", 26, 0, "Aired");
            var a5 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a6 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a7 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a8 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a9 = new Anime("Slayers", "TV", 30, 4, "Airing");

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
    }
}
