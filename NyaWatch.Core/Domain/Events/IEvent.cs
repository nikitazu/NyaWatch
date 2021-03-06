using System;
using System.Collections.Generic;

namespace NyaWatch.Core.Domain.Events
{
	/// <summary>
	/// Event interface.
	/// </summary>
	public interface IEvent : IEntity
	{
		/// <summary>
		/// Show title if present.
		/// </summary>
		/// <value>The title.</value>
		string Title { get; set; }

		/// <summary>
		/// Show message if present.
		/// </summary>
		/// <value>The message.</value>
		string Message { get; set; }

		/// <summary>
		/// Open category if present.
		/// </summary>
		/// <value>The category.</value>
		string Category { get; set; }

		/// <summary>
		/// Select anime if present.
		/// </summary>
		/// <value>The anime I.</value>
		Guid AnimeID { get; set; }

		/// <summary>
		/// Event creation date.
		/// </summary>
		/// <value>Event creation date.</value>
		DateTime Created { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NyaWatch.Core.Domain.Events.IEvent"/> is watched.
		/// </summary>
		/// <value><c>true</c> if watched; otherwise, <c>false</c>.</value>
		bool Watched { get; set; }
	}
}

