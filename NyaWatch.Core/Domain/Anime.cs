﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NyaWatch.Core.Data;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Anime use cases.
    /// </summary>
    public static class Anime
    {
		/// <summary>
		/// Save all changes.
		/// </summary>
		public static void Save()
		{
			Init.Storage.Save ();
		}

		/// <summary>
		/// Loads last save.
		/// </summary>
		public static void Load()
		{
			Init.Storage.Load ();
		}

		/// <summary>
		/// Drops all saved data.
		/// </summary>
		public static void Drop()
		{
			Init.Storage.Drop ();
		}

        /// <summary>
        /// Find all anime in category.
        /// </summary>
        /// <typeparam name="T">Anime data type</typeparam>
        /// <param name="category">Category.</param>
        /// <returns>List of anime.</returns>
        public static List<T> Find<T>(Categories category) where T : class, IAnime, new()
        {
            var query = Init.Storage.SelectItems(category.ToString())
                .Select(kv =>
                    {
                        T anime = Activator.CreateInstance<T>();
                        anime.ID = kv.Key;
                        DeserializeAnime(kv.Value, anime);
                        return anime;
                    });

            var sorted = from anime in query
                         orderby !anime.Pinned, anime.Title
                         select anime;

            return sorted.ToList();
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

        /// <summary>
        /// Deserialize item to anime.
        /// </summary>
        /// <exception cref="DeserializeFailedException" />
        static void DeserializeAnime(IDictionary<string, string> item, IAnime anime)
        {
            try
            {
                anime.Title = item["title"];
                anime.Episodes = int.Parse (item["episodes"]);
                anime.Watched = int.Parse (item["watched"]);
                anime.Type = item["type"];
                anime.Status = item["status"];
                anime.Pinned = bool.Parse (item["pinned"]);
                anime.Year = int.Parse (item["year"]);
                anime.AiringStart = item["airingStart"].DeserializeDate ();
                anime.AiringEnd = item["airingEnd"].DeserializeDate ();
                anime.ImageUrl = item["imageUrl"];
                anime.ImagePath = item["imagePath"];
            }
            catch (Exception e)
            {
                throw new DeserializeFailedException (item, e);
            }
        }

        /// <summary>
        /// Serialize anime.
        /// </summary>
        /// <exception cref="SerializeFailedException" />
        static IDictionary<string, string> SerializeAnime(IAnime anime)
        {
            try
            {
                var item = new Dictionary<string, string> ();
                item["title"] = anime.Title;
                item["episodes"] = anime.Episodes.ToString ();
                item["watched"] = anime.Watched.ToString ();
                item["type"] = anime.Type;
                item["status"] = anime.Status;
                item["pinned"] = anime.Pinned.ToString ();
                item["year"] = anime.Year.ToString ();
                item["airingStart"] = anime.AiringStart.SerializeDate ();
                item["airingEnd"] = anime.AiringEnd.SerializeDate ();
                item["imageUrl"] = anime.ImageUrl ?? string.Empty;
                item["imagePath"] = anime.ImagePath ?? string.Empty;
                return item;
            }
            catch (Exception e)
            {
                throw new SerializeFailedException (anime, e);
            }
        }
    }
}
