using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.ViewModels
{
    public class VideoListViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
