using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NyaWatch.Core.Domain
{
    public class ImageLoader
    {
        public void LoadImageForAnime(IAnime anime)
        {
            if (anime == null) { throw new ArgumentNullException ("anime"); }
            if (string.IsNullOrWhiteSpace (anime.ImageUrl)) { return; }

            var path = Files.ImagePath(anime);
            using (var web = new WebClient()) {
                web.DownloadFile (anime.ImageUrl, path);
                anime.ImagePath = path;
            }
        }
    }
}
