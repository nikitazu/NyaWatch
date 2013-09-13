using System;

namespace NyaWatch.Core.Domain
{
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
		/// Show new anime episode number that just came out if present.
		/// </summary>
		/// <value>The anime episode.</value>
		int? AnimeEpisode { get; set; }

		/// <summary>
		/// Show new anime premiere that just came out if present.
		/// </summary>
		/// <value>The premiere.</value>
		/// <remarks>Premiere should be exported to PlanToWatch category.</remarks>
		IAnime Premiere { get; set; }
	}
}

