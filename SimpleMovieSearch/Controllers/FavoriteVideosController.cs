using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class FavoriteVideosController : Controller
    {
        private readonly AppDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public FavoriteVideosController(AppDBContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AllFavoriteVideos()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var user = _db.User.Include(x => x.FavoriteVideos).FirstOrDefault(x => x.Email == User.Identity.Name);

                var userFavoriteViewModel = new UserFavoriteViewModel
                {
                    FavoriteVideos = user.FavoriteVideos.ToList()
                };

                return View(userFavoriteViewModel);
            }

            return Content("Please just register");
        }

    }
}
