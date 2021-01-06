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
        private readonly Data.AppDBContext _content;

        public FavoriteVideosController(Data.AppDBContext content)
        {
            _content = content;
        }

        [HttpGet]
        public IActionResult AllFavoriteVideos()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var userFavoriteViewModel = new UserFavoriteViewModel 
                {
                    User = _content.User.FirstOrDefault(x => x.Email == User.Identity.Name), 
                    FavoriteVideos = _content.User.Include(x => x.FavoriteVideos).FirstOrDefault(x => x.Email == User.Identity.Name).FavoriteVideos.ToList() 
                };

                return View(userFavoriteViewModel);
            }

            return Content("Please just register");
        }

    }
}
