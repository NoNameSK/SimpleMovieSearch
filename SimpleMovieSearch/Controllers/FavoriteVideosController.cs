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
        private readonly Data.AppDBContext _db;

        public FavoriteVideosController(Data.AppDBContext сontext)
        {
            _db = сontext;
        }

        [HttpGet]
        public IActionResult AllFavoriteVideos()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var user = _db.User.Include(x => x.FavoriteVideos).FirstOrDefault(x => x.Email == User.Identity.Name);

                var userFavoriteViewModel = new UserFavoriteViewModel 
                {
                    User = user, 
                    FavoriteVideos = user.FavoriteVideos.ToList() 
                };

                return View(userFavoriteViewModel);
            }

            return Content("Please just register");
        }

    }
}
