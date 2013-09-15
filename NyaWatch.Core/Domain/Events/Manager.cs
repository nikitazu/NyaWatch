using System;
using System.Collections.Generic;
using System.Linq;
using NyaWatch.Core.Data;

namespace NyaWatch.Core.Domain.Events
{
	public static class Manager
	{
		public const string EventsCategory = "events";

		public static List<IEvent> LoadAll()
		{
			var query = Init.Storage.SelectItems (EventsCategory)
				.Select(kv =>
				        {
					var eventTypeName = kv.Value["type"];
					IEvent evt = null;
					switch (eventTypeName) {
					case "PremiereEvent": evt = Activator.CreateInstance<PremiereEvent>(); break;
					case "NewTorrentsEvent": evt = Activator.CreateInstance<NewTorrentsEvent>(); break;
					case "NewEpisodesEvent": evt = Activator.CreateInstance<NewEpisodesEvent>(); break;
					default:
						throw new NotSupportedException("Unknown event type: " + eventTypeName);
					}
					evt.ID = kv.Key;
					DeserializeEvent(kv.Value, evt);
					return evt;
				});

			var sorted = from evt in query orderby evt.Created select evt;

			return sorted.ToList();
		}

		public static void PutTestEvents()
		{
			Put (new NewEpisodesEvent () {
				Title = "Railgun 23"
			});
			Put (new NewEpisodesEvent () {
				Title = "NGE 56"
			});
			Put (new NewTorrentsEvent () {
				Title = "Railgun 23"
			});
			Put (new PremiereEvent () {
				Title = "Railgun: The Movie"
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
				evt.Title = item.OptionalString("title");
				evt.Message = item.OptionalString("message");
				evt.Category = item.OptionalString("category");

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
				item["type"] = evt.GetType().Name.ToString();
				item["created"] = evt.Created.ToString("yyyy-MM-dd-hh:mm:ss");
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

