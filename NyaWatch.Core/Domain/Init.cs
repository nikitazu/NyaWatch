using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core.Domain
{
    /// <summary>
    /// Initialization of domain logic.
    /// </summary>
    static class Init
    {
        /// <summary>
        /// Data storage.
        /// </summary>
        public static Data.IStorage Storage { get; private set; }

        /// <summary>
        /// Initialization.
        /// </summary>
        static Init()
        {
            Storage = new Data.MemoryStorage();

            foreach (var cat in Enum.GetNames(typeof(Categories)))
            {
                Storage.AddCategory(cat);
            }
        }
    }
}
