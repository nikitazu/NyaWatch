using System;
using System.Collections.Generic;
using System.Linq;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// New torrents found.
	/// </summary>
	public class NewTorrentsEvent : BaseEvent, IEvent
	{
		List<string> _torrents;
	}
}

