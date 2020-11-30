using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }

        public ViewResult Index()
        {
            //var homeVideos = new HomeViewModel
            //{
            //    favoriteVideo = _videoRepository.GetFavoriteVideos
            //};
            return View(/*homeVideos*/);
        }
    }
}
