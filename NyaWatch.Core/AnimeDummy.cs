using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NyaWatch.Core
{
    public class AnimeDummy : Domain.IAnime
    {
        public Guid ID { get; set; }
		public Domain.IRoot Root { get; set; }
        public string Title { get; set; }
		public List<string> OtherTitles { get; set; }
        public int Episodes { get; set; }
        public int Watched { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Year { get; set; }
        public DateTime? AiringStart { get; set; }
        public DateTime? AiringEnd { get; set; }
        public string ImagePath { get; set; }
        public string ImageUrl { get; set; }
        public bool Pinned { get; set; }
    }
}
