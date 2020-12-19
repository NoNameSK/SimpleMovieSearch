using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class FavoriteVideosController : Controller
    {
        private readonly AppDBContent _content;

        public FavoriteVideosController(AppDBContent content)
        {
            _content = content;
        }

        [HttpGet]
        public async Task<IActionResult> AllFavoriteVideos()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var user = _content.User.FirstOrDefault(x => x.Email == User.Identity.Name);
                var videos = _content.User.Include(x => x.FavoriteVideos).FirstOrDefault(x => x.Email == User.Identity.Name).FavoriteVideos.ToList();

                var userFavoriteViewModel = new UserFavoriteViewModel { User = user, FavoriteVideos = videos };

                return View(userFavoriteViewModel);
            }

            return Content("Please just register");
        }

    }
}
