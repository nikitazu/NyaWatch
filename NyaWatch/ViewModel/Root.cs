using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MonoMac.Foundation;
using MonoMac.AppKit;

using NyaWatch.Cocoa;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.ViewModel
{
	public class Root : NSObject
	{
		[Export("animes")]
		//public List<Anime> Animes { get; set; }
		public NSArray Animes { get; set; }

		[Export("selectedAnime")]
		public Anime SelectedAnime { get; set; }


		public Root ()
		{
			Animes = cd.Anime.Find<ViewModel.Anime> (cd.Categories.Watching).ToNSArray ();
		}
	}
}

