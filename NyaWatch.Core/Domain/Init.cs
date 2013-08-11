using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Initialization of domain logic.
    /// </summary>
    static class Init
    {
        /// <summary>
        /// Data storage.
        /// </summary>
        public static Data.IStorage Storage { get; private set; }

        /// <summary>
        /// Initialization.
        /// </summary>
        static Init()
        {
            Storage = new Data.MemoryStorage();

            foreach (var cat in Enum.GetNames(typeof(Categories)))
            {
                Storage.AddCategory(cat);
            }

            CreateTestAnimes();
        }

        static void CreateTestAnimes()
        {
            Anime.PutDynamic(Categories.PlanToWatch,
                new
                {
                    Title = "Attack on titan - The movie",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Not yet aired",
                    Watched = 0,
                    Torrents = 0,
                    Pinned = false
                });

            Anime.PutDynamic(Categories.PlanToWatch,
                new
                {
                    Title = "Murdoc Scramble: The third exaustion",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Not yet aired",
                    Watched = 0,
                    Pinned = true
                });

            Anime.PutDynamic(Categories.Watching,
                new
                {
                    Title = "Bleach",
                    Type = "TV",
                    Episodes = 365,
                    Status = "Aired",
                    Watched = 356,
                    Pinned = false
                });

            Anime.PutDynamic(Categories.Watching,
                new
                {
                    Title = "Naruto: Shippuuden",
                    Type = "TV",
                    Episodes = 100500,
                    Status = "Airing",
                    Watched = 666,
                    Pinned = false
                });

            Func<string, dynamic> tvCompleted = title => new
            {
                Title = title,
                Type = "TV",
                Episodes = 26,
                Status = "Aired",
                Watched = 26,
                Pinned = false
            };

            Anime.PutDynamic(Categories.Completed, tvCompleted("Slayers"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Neon Genesis Evangelion"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Slayers: Next"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Slayers: Try"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Slayers: Perfect"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Lost Universe"));
            Anime.PutDynamic(Categories.Completed, tvCompleted("Great Teacher Onizuka"));
            Anime.PutDynamic(Categories.Completed,
                new
                {
                    Title = "Asura",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Aired",
                    Watched = 1,
                    Pinned = false
                });

            Anime.PutDynamic(Categories.OnHold,
                new
                {
                    Title = "Slayers: Gourgeous",
                    Type = "OVA",
                    Episodes = 3,
                    Status = "Aired",
                    Watched = 2,
                    Pinned = false
                });

            Anime.PutDynamic(Categories.Dropped,
                new
                {
                    Title = "Some shit",
                    Type = "TV",
                    Episodes = 26,
                    Status = "Airing",
                    Watched = 5,
                    Pinned = false
                });
        }
    }
}
