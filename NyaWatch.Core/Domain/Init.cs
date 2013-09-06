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
        public static Data.IStorageOnDisk Storage { get; private set; }

        /// <summary>
        /// Initialization.
        /// </summary>
        static Init()
        {
			Storage = new Data.XmlStorage (Files.DatabasePath);

            foreach (var cat in Enum.GetNames(typeof(Categories)))
            {
                Storage.AddCategory(cat);
            }

			if (Storage.Exists ()) {
				Storage.Load ();
			} else  {
				CreateTestAnimes ();
				Storage.Save ();
			}
        }

        static void CreateTestAnimes()
        {
            Anime.Put(Categories.PlanToWatch,
                new AnimeDummy
                {
                    Title = "Attack on titan - The movie",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Not yet aired",
                    Watched = 0,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Anime.Put(Categories.PlanToWatch,
                new AnimeDummy
                {
                    Title = "Murdoc Scramble: The third exaustion",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Not yet aired",
                    Watched = 0,
                    Pinned = true,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Anime.Put(Categories.Watching,
                new AnimeDummy
                {
                    Title = "Bleach",
                    Type = "TV",
                    Episodes = 365,
                    Status = "Aired",
                    Watched = 356,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Anime.Put(Categories.Watching,
                new AnimeDummy
                {
                    Title = "Naruto: Shippuuden",
                    Type = "TV",
                    Episodes = 100500,
                    Status = "Airing",
                    Watched = 666,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Func<string, IAnime> tvCompleted = title => new AnimeDummy
            {
                Title = title,
                Type = "TV",
                Episodes = 26,
                Status = "Aired",
                Watched = 26,
                Pinned = false,
                Year = 2010,
                ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
            };

            Anime.Put(Categories.Completed, tvCompleted("Slayers"));
            Anime.Put(Categories.Completed, tvCompleted("Neon Genesis Evangelion"));
            Anime.Put(Categories.Completed, tvCompleted("Slayers: Next"));
            Anime.Put(Categories.Completed, tvCompleted("Slayers: Try"));
            Anime.Put(Categories.Completed, tvCompleted("Slayers: Perfect"));
            Anime.Put(Categories.Completed, tvCompleted("Lost Universe"));
            Anime.Put(Categories.Completed, tvCompleted("Great Teacher Onizuka"));
            Anime.Put(Categories.Completed,
                new AnimeDummy
                {
                    Title = "Asura",
                    Type = "Movie",
                    Episodes = 1,
                    Status = "Aired",
                    Watched = 1,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Anime.Put(Categories.OnHold,
                new AnimeDummy
                {
                    Title = "Slayers: Gourgeous",
                    Type = "OVA",
                    Episodes = 3,
                    Status = "Aired",
                    Watched = 2,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });

            Anime.Put(Categories.Dropped,
                new AnimeDummy
                {
                    Title = "Some shit",
                    Type = "TV",
                    Episodes = 26,
                    Status = "Airing",
                    Watched = 5,
                    Pinned = false,
                    Year = 2010,
                    ImageUrl = "http://www.world-art.ru/animation/img/1000/395/1.jpg"
                });
        }
    }
}
