using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Windows.ViewModel
{
    public class Root
    {
        public List<Anime> Animes { get; private set; }

        public Root()
        {
            Animes = new List<Anime>();

            var a1 = new Anime("Slayers: Excellent", "OVA", 20, 0, "Aired");
            var a2 = new Anime("Asura", "Movie", 25, 1, "Not yet aired");
            var a3 = new Anime("Slayers", "TV", 30, 4, "Airing");
            var a4 = new Anime("Neon Genesis Evangelion", "TV", 26, 0, "Aired");

            Animes.AddRange(new List<Anime> { a1, a2, a3, a4 });
        }
    }
}
