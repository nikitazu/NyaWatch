using System;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// Base event implementation.
	/// </summary>
	public abstract class BaseEvent : IEvent
	{
		public Guid ID { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public string Category { get; set; }
		public Guid AnimeID { get; set; }
		public DateTime Created { get; set; }

		public BaseEvent ()
		{
			Created = DateTime.Now;
		}
	}
}
