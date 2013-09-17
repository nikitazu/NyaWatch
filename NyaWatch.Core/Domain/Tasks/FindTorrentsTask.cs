using System;
using FluentScheduler;

namespace NyaWatch.Core.Domain.Tasks
{
	public class FindTorrentsTask : ITask
	{
		const string TorrentsLink = "http://www.nyaa.se/?page=rss&term=railgun+s+20";

		public FindTorrentsTask ()
		{
			Console.WriteLine ("task find torrents: inititalize begin");

			Console.WriteLine ("task find torrents: inititalize end");
		}

		#region ITask implementation

		public void Execute ()
		{
			Console.WriteLine ("task find torrents: execute begin");

			var torrents = new Parsing.NyaaTorrentParser ().ParseTorrentsFromWeb (TorrentsLink);
			foreach (var torrent in torrents) {
				Console.WriteLine ("found torrent: {0} s:{1} l:{2}", torrent ["title"], torrent ["seeders"], torrent ["leechers"]);
			}

			Console.WriteLine ("task find torrents: execute end");
		}

		#endregion
	}
}

