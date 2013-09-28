using System;
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
        public static Guid ParseFromWeb (Parsing.IParser parser, string url)
        {
            var anime = new AnimeDummy ();
            var animeData = parser.ParseAnimeFromWeb (url);
            DeserializeAnime (animeData, anime);
            return Put (Categories.PlanToWatch, anime);
        }

		public static void LoadImage(IAnime anime)
		{
			new ImageLoader ().LoadImageForAnime (anime);
			Update (anime);
		}

		public static void Increment(IAnime anime)
		{
			if (anime.Watched < anime.Episodes) {
				anime.Watched += 1;
				Update (anime);
			}
		}

		public static void Decrement(IAnime anime)
		{
			if (anime.Watched > 0) {
				anime.Watched -= 1;
				Update (anime);
			}
		}

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

        public static Guid Move(Categories target, IAnime anime)
        {
			Init.Storage.MoveItem (anime.ID, target.ToString ());
			return anime.ID;
        }

        public static void Update(IAnime anime)
        {
            var item = SerializeAnime(anime);
            Init.Storage.UpdateItem(anime.ID, item);
        }

        /// <summary>
        /// Deserialize item to anime.
        /// </summary>
        /// <exception cref="DeserializeFailedException" />
        static void DeserializeAnime(IDictionary<string, string> item, IAnime anime)
        {
            try
            {
                anime.Title = item.RequireString ("title");
                anime.Episodes = item.OptionalInt ("episodes") ?? 0;
                anime.Watched = item.OptionalInt ("watched") ?? 0;
                anime.Type = item.RequireString ("type");
                anime.Status = item.OptionalString ("status") ?? AnimeAiringStatus.Unknown;
                anime.Pinned = item.OptionalBool ("pinned") ?? false;
                anime.Year = item.RequireInt ("year");
                anime.AiringStart = item.OptionalDate ("airingStart");
                anime.AiringEnd = item.OptionalDate ("airingEnd");
                anime.ImageUrl = item.OptionalString ("imageUrl") ?? string.Empty;
                anime.ImagePath = item.OptionalString ("imagePath") ?? string.Empty;

				var otherTitles = item.OptionalString ("otherTitles");
				anime.OtherTitles = otherTitles == null ? new List<string>() : otherTitles.Split(',').ToList();
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
				item["otherTitles"] = string.Join(",", anime.OtherTitles);
                return item;
            }
            catch (Exception e)
            {
                throw new SerializeFailedException (anime, e);
            }
        }
    }
}
