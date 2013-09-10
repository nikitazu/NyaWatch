using System;
using System.Collections.Generic;
using System.Linq;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.ViewModel
{
	public class Root : cd.IRoot
	{
		public cd.Categories SelectedCategory { get; set; }
	}
}

