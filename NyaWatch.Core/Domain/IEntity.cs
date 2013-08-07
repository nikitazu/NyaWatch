using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Entity, that can be put in storage and found by its identificator.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        Guid ID { get; set; }
    }
}
