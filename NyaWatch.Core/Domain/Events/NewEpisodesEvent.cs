using System;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// New episodes announced by publisher.
	/// </summary>
	public class NewEpisodesEvent : BaseEvent, IEvent
	{
		int _episodeNumber;
	}
}

