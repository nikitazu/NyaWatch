using System;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// Premiere announced.
	/// </summary>
	public class PremiereEvent : BaseEvent, IEvent
	{
		IAnime _premiere;
	}
}

