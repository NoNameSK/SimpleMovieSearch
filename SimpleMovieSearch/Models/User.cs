using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Models
{
    public class User : IdentityUser
    {
        public List<Video> FavoriteVideos { get; set; } = new List<Video>();
    }
}
