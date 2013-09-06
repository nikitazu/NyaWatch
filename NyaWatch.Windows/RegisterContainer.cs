using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using NyaWatch.Core.UI;
using NyaWatch.Windows.UI;

namespace NyaWatch.Windows
{
    /// <summary>
    /// Register IoC container.
    /// </summary>
    public static class RegisterContainer
    {
        /// <summary>
        /// Register IoC container.
        /// </summary>
        public static void Init()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(s =>
                {
                    s.AddAllTypesOf(typeof(IMessageBox));
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                });
            });
            
            Console.WriteLine(ObjectFactory.WhatDoIHave());
        }
    }
}
