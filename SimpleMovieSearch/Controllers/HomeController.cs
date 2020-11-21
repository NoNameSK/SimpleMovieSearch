using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Services.Interfaces;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllVideo _videoRepository;

        public HomeController(IAllVideo videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public ViewResult Index()
        {
            var homeVideos = new HomeViewModel
            {
                favoriteVideo = _videoRepository.GetFavoriteVideos
            };
            return View(homeVideos);
        }
    }
}
