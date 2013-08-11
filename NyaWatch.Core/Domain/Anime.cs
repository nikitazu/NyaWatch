using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Anime use cases.
    /// </summary>
    public static class Anime
    {
        /// <summary>
        /// Find all anime in category.
        /// </summary>
        /// <typeparam name="T">Anime data type</typeparam>
        /// <param name="category">Category.</param>
        /// <returns>List of anime.</returns>
        public static List<T> Find<T>(Categories category) where T : class, IAnime, new()
        {
            return Init.Storage.SelectItems(category.ToString())
                .Select(kv =>
                    {
                        T anime = Activator.CreateInstance<T>();
                        anime.ID = kv.Key;
                        DeserializeAnime(kv.Value, anime);
                        return anime;
                    })
                .OrderBy(a => a.Title)
                .ToList();
        }

        /// <summary>
        /// Put anime into category.
        /// </summary>
        /// <param name="category">Category.</param>
        /// <param name="anime">Anime.</param>
        /// <returns>New identificator for anime.</returns>
        public static Guid Put(Categories category, IAnime anime)
        {
            var item = SerializeAnime(anime);
            var id = Init.Storage.AddItem(category.ToString(), item);
            anime.ID = id;
            return id;
        }

        internal static Guid PutDynamic(Categories category, dynamic anime)
        {
            var item = new Dictionary<string, string>();
            item["title"] = anime.Title;
            item["episodes"] = anime.Episodes.ToString();
            item["watched"] = anime.Watched.ToString();
            item["type"] = anime.Type;
            item["status"] = anime.Status;
            return Init.Storage.AddItem(category.ToString(), item);
        }

        public static Guid Move(Categories source, Categories target, IAnime anime)
        {
            var item = SerializeAnime(anime);
            var id = Init.Storage.AddItem(target.ToString(), item);
            Init.Storage.RemoveItem(source.ToString(), anime.ID);
            anime.ID = id;
            return id;
        }

        public static void Save(Categories category, IAnime anime)
        {
            var item = SerializeAnime(anime);
            Init.Storage.UpdateItem(category.ToString(), anime.ID, item);
        }

        static void DeserializeAnime(IDictionary<string, string> item, IAnime anime)
        {
            anime.Title = item["title"];
            anime.Episodes = int.Parse(item["episodes"]);
            anime.Watched = int.Parse(item["watched"]);
            anime.Type = item["type"];
            anime.Status = item["status"];
        }

        static IDictionary<string, string> SerializeAnime(IAnime anime)
        {
            var item = new Dictionary<string, string>();
            item["title"] = anime.Title;
            item["episodes"] = anime.Episodes.ToString();
            item["watched"] = anime.Watched.ToString();
            item["type"] = anime.Type;
            item["status"] = anime.Status;
            return item;
        }
    }
}
