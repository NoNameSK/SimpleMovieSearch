using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using SimpleMovieSearch.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SimpleMovieSearch.Helper;

namespace SimpleMovieSearch.Controllers
{
    public class VideosController : Controller
    {

        private readonly AppDBContent _content;

        public VideosController( AppDBContent content)
        {

            _content = content;
        }

        [Route("Videos/List")]
        [Route("Videos/List/{category}")]
        public async Task<IActionResult> List()
        {
            //string _author = author;
            //IEnumerable<Video> videos = null;
            //string VideoAuthor = "";
            //if (string.IsNullOrEmpty(author))
            //{
            //    videos = _allVideos.Videos.OrderBy(i => i.Id);
            //}
            //else
            //{
            //    if (string.Equals("Mikle", author, StringComparison.OrdinalIgnoreCase))
            //    {
            //        videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Майкл")).OrderBy(i => i.Id);
            //    }
            //    else if (string.Equals("Gay", author, StringComparison.OrdinalIgnoreCase))
            //    {
            //        videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Гай")).OrderBy(i => i.Id);
            //    }
            //    else if (string.Equals("Deny", author, StringComparison.OrdinalIgnoreCase))
            //    {
            //        videos = _allVideos.Videos.Where(i => i.Author.Name.Equals("Дени")).OrderBy(i => i.Id);
            //    }

            //    VideoAuthor = _author;
            //}
            //var video= new VideoListViewModel
            //{
            //    AllVideos = videos,
            //    VideoAuthor = VideoAuthor

            //};


            ViewBag.Title = "Videos";

            return View(await _content.Video.ToListAsync());
        }

        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Video());
            else
            {
                var video = await _content.Video.FindAsync(id);
                if (video == null)
                {
                    return NotFound();
                }
                return View(video);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Image,Title,ShortDescription,LongDescription,IsFavorites,AuthorId")] Video video)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _content.Add(video);
                    await _content.SaveChangesAsync();

                }
                else
                {
                    try
                    {
                        _content.Update(video);
                        await _content.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VideoExists(video.Id))
                        { return NotFound(); }
                        else
                        { throw; }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "List", _content.Video.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", video) });
        }

        private bool VideoExists(int id)
        {
            return _content.Video.Any(e => e.Id == id);
        }
    }
}
