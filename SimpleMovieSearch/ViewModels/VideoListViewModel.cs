using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<Video> Videos { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Genres { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<int> SelectedGenreIds { get; set; }
    }
}
