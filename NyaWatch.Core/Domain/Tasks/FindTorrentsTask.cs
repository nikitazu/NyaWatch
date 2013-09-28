using System;
using System.Collections.Generic;
using System.Linq;
using FluentScheduler;

namespace NyaWatch.Core.Domain.Tasks
{
	public class FindTorrentsTask : ITask
	{
		const string TorrentsLink = "http://www.nyaa.se/?page=rss&term=";

		public FindTorrentsTask ()
		{
			Console.WriteLine ("task find torrents: inititalize begin");

			Console.WriteLine ("task find torrents: inititalize end");
		}

		#region ITask implementation

		public void Execute ()
		{
			Console.WriteLine ("task find torrents: execute begin");

			var watchingAnimes = Domain.Anime.Find<Core.AnimeDummy> (Categories.Watching);
			foreach (IAnime anime in watchingAnimes) {
				var searchTitle = anime.OtherTitles.Any () ? anime.OtherTitles.First () : anime.Title;
				var queryTerm = searchTitle.Replace (' ', '+') + "+" + (anime.Watched + 1).ToString ();
				Console.WriteLine ("QUERY TERM = {0}", queryTerm);

				var torrents = new Parsing.NyaaTorrentParser ().ParseTorrentsFromWeb (TorrentsLink + queryTerm);
				
				foreach (var torrent in torrents) {
					Console.WriteLine ("found torrent: {0} s:{1} l:{2}", torrent ["title"], torrent ["seeders"], torrent ["leechers"]);
				}

				/*if (torrents.Any ()) {
					var evt = new Core.Domain.Events.NewTorrentsEvent {
						Title = "Railgun S",
					};
				}*/
			}

			Console.WriteLine ("task find torrents: execute end");
		}

		#endregion
	}
}

