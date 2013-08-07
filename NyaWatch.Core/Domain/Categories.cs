using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Anime categories.
    /// </summary>
    public enum Categories
    {
        /// <summary>
        /// Anime we are only planning to watch later.
        /// </summary>
        PlanToWatch,

        /// <summary>
        /// Anime we are watching now.
        /// </summary>
        Watching,

        /// <summary>
        /// Anime we watched allready.
        /// </summary>
        Completed,

        /// <summary>
        /// Anime we put on hold, to watch later.
        /// </summary>
        OnHold,

        /// <summary>
        /// Anime we ceased watching, because it sucks.
        /// </summary>
        Dropped
    }
}
