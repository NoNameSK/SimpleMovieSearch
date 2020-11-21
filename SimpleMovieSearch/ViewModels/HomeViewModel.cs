using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Video> favoriteVideo { get; set; }
    }
}
