using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.ViewModels
{
    public class CreateVideoViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public List<VideoGenres> VideoGenres { get; set; }
        public int[] GenreIds { get; set; }
    }
}
