using System;
using System.Collections.Generic;
using System.Linq;
using NyaWatch.Core.Data;

namespace NyaWatch.Core.Domain.Events
{
	public static class Manager
	{
		public const string EventsCategory = "events";
		public static string AssemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;

		public static List<IEvent> LoadAll()
		{
			var query = Init.Storage.SelectItems (EventsCategory) .Select(kv => {
				var evt = Activator.CreateInstance(AssemblyName, kv.Value["type"]).Unwrap() as IEvent;
				evt.ID = kv.Key;
				DeserializeEvent(kv.Value, evt);
				return evt;
			});

			var sorted = from evt in query orderby evt.Watched, evt.Created select evt;
			return sorted.ToList();
		}

		public static void PutTestEvents()
		{
			Put (new NewEpisodesEvent () {
				Title = "Railgun 23"
			});
			Put (new NewEpisodesEvent () {
				Title = "NGE 56",
				Watched = true
			});
			Put (new PremiereEvent () {
				Title = "Railgun: The Movie"
			});
			Put (new PremiereEvent () {
				Title = "Railgun: The OVA",
				Watched = true
			});
			Put (new InfoEvent () {
				Title = "You can do it"
			});
			Put (new InfoEvent () {
				Title = "New pajamas",
				Watched = true
			});
		}

		public static Guid Put(IEvent evt)
		{
			var item = SerializeAnime(evt);
			var id = Init.Storage.AddItem(EventsCategory, item);
			evt.ID = id;
			return id;
		}

		static void DeserializeEvent(IDictionary<string, string> item, IEvent evt)
		{
			try
			{
				evt.Title = item.OptionalString("title") ?? string.Empty;
				evt.Message = item.OptionalString("message") ?? string.Empty;
				evt.Category = item.OptionalString("category") ?? string.Empty;
				evt.Watched = item.OptionalBool("watched") ?? false;

				var animeID = item.OptionalString("anime-id");
				if (!string.IsNullOrWhiteSpace(animeID)) {
					evt.AnimeID = Guid.Parse(animeID);
				}

				evt.Created = DateTime.ParseExact(item["created"], "yyyy-MM-dd-hh:mm:ss", null);
			}
			catch (Exception e)
			{
				throw new DeserializeFailedException (item, e);
			}
		}

		static IDictionary<string, string> SerializeAnime(IEvent evt)
		{
			try
			{
				var item = new Dictionary<string, string> ();
				item["title"] = evt.Title;
				item["message"] = evt.Message ?? string.Empty;
				item["category"] = evt.Category ?? string.Empty;
				item["anime-id"] = evt.AnimeID.ToString();
				item["type"] = evt.GetType().FullName;
				item["created"] = evt.Created.ToString("yyyy-MM-dd-hh:mm:ss");
				item["watched"] = evt.Watched.ToString();
				return item;
			}
			catch
			{
				throw;
				//throw new SerializeFailedException (evt, e);
			}
		}
	}
}

