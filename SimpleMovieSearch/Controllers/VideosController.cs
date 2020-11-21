using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Models;
using SimpleMovieSearch.Services.Interfaces;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class VideosController : Controller
    {
        private readonly IAllVideo _allVideos;
        private readonly IVideoAuthor _allAuthors;

        public VideosController(IAllVideo iAllVideos, IVideoAuthor iVideosAuthor)
        {
            _allVideos = iAllVideos;
            _allAuthors = iVideosAuthor;
        }

        [Route("Videos/List")]
        [Route("Videos/List/{category}")]
        public ViewResult List(string author)
        {
            string _author = author;
            IEnumerable<Video> videos = null;
            string VideoAuthor = "";
            if (string.IsNullOrEmpty(author))
            {
                videos = _allVideos.Videos.OrderBy(i => i.Id);
            }
            else
            {
                if (string.Equals("Mikle", author, StringComparison.OrdinalIgnoreCase))
                {
                    videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Майкл")).OrderBy(i => i.Id);
                }
                else if (string.Equals("Gay", author, StringComparison.OrdinalIgnoreCase))
                {
                    videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Гай")).OrderBy(i => i.Id);
                }
                else if (string.Equals("Deny", author, StringComparison.OrdinalIgnoreCase))
                {
                    videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Дени")).OrderBy(i => i.Id);
                }

                VideoAuthor = _author;
            }
            var videoObject = new VideoListViewModel
            {
                AllVideos = videos,
                VideoAuthor = VideoAuthor

            };

            ViewBag.Title = "Videos";

            return View(videoObject);
        }
    }
}
