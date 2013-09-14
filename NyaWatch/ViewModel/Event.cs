using System;
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
		}

		[Export("title")]
		public string Title {
			get {
				return _event.Title;
			}
			set {
				_event.Title = value;
			}
		}
	}
}

