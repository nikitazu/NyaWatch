using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using MonoMac.AppKit;

using cd = NyaWatch.Core.Domain;

namespace NyaWatch.ViewModel
{
	public class Event : NSObject
	{
		cd.Events.IEvent _event;

		public Event (cd.Events.IEvent evt)
		{
			_event = evt;
			ImagePath = EventImageLoader.ImagePathFor (evt);
		}

		[Export("title")]
		public string Title {
			get { return _event.Title; }
		}
		
		[Export("imagePath")]
		public string ImagePath { get; set; }
	}

	static class EventImageLoader
	{
		static Dictionary<Type, string> _resources;

		static EventImageLoader()
		{
			_resources = new Dictionary<Type, string> ();
			_resources [typeof(cd.Events.NewEpisodesEvent)] = "Events/episode";
			_resources [typeof(cd.Events.NewTorrentsEvent)] = "Events/torrent";
			_resources [typeof(cd.Events.PremiereEvent)] = "Events/premiere";
			//Events/info
		}

		public static string ImagePathFor(cd.Events.IEvent evt)
		{
			string path = "Events/info";
			try {
				path = _resources[evt.GetType()];
			} catch (KeyNotFoundException) {
				// nothing
			}
			return NSBundle.MainBundle.PathForResource (path, "png");
		}
	}
}

