using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Models
{
    public class VideoGenres
    {
        public int VideosId { get; set; }
        public Video Video { get; set; }
        public int GenresId { get; set; }
        public Genre Genres { get; set; }
    }
}
