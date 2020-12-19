using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Video> Videos { get; set; }
        //public List<VideoGenres> VideoGenres { get; set; }
    }
}
