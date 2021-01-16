using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.ViewModels
{
    public class UserFavoriteViewModel
    {
        public User User { get; set; }
        public List<Video> FavoriteVideos { get; set; }
    }
}
