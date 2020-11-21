using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.ViewModels
{
    public class VideoListViewModel
    {
        public IEnumerable<Video> AllVideos { get; set; }
        public string VideoAuthor { get; set; }
    }
}
