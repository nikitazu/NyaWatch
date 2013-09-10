using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Anime interface.
    /// </summary>
    public interface IAnime : IEntity
    {
		/// <summary>
		/// Gets or sets the root.
		/// </summary>
		/// <value>The root.</value>
		IRoot Root { get; set; }

        /// <summary>
        /// Anime series title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Total number of episodes of anime series.
        /// </summary>
        int Episodes { get; set; }

        /// <summary>
        /// How much episodes we have watched.
        /// </summary>
        int Watched { get; set; }

        /// <summary>
        /// Anime type (TV, OVA, Movie).
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Anime status (Not yet aired, Airing, Aired).
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Year of anime airing start.
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// Date of anime airing start.
        /// </summary>
        DateTime? AiringStart { get; set; }

        /// <summary>
        /// Date of anime airing end.
        /// </summary>
        DateTime? AiringEnd { get; set; }

        /// <summary>
        /// Path to image on disk (if NULL default image will be used).
        /// </summary>
        string ImagePath { get; set; }

        /// <summary>
        /// Image url to download it from web (if NULL default image will be used);
        /// </summary>
        string ImageUrl { get; set; }

        /// <summary>
        /// Pinned anime is shown first in the interface.
        /// </summary>
        bool Pinned { get; set; }
    }
}
