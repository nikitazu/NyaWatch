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
				FindTorrents (anime.Title, anime);
				foreach (var title in anime.OtherTitles) {
					FindTorrents (title, anime);
				}
			}

			Console.WriteLine ("task find torrents: execute end");
		}

		void FindTorrents(string searchTitle, IAnime anime)
		{
			var series = anime.Watched + 1;
			var queryTerm = searchTitle.Replace (' ', '+') + "+" + series.ToString ();
			var torrents = new Parsing.NyaaTorrentParser ().ParseTorrentsFromWeb (TorrentsLink + queryTerm);
			/*
			foreach (var torrent in torrents) {
				Console.WriteLine ("found torrent: {0} s:{1} l:{2}", torrent ["title"], torrent ["seeders"], torrent ["leechers"]);
			}*/

			if (torrents.Any ()) {
				var evt = new Core.Domain.Events.NewTorrentsEvent(torrents, anime) {
					Title = queryTerm
				};
				Console.WriteLine (evt);
				Console.WriteLine ();
				Console.WriteLine ();
			}
		}

		#endregion
	}
}

