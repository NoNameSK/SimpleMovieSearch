using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SimpleMovieSearch.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SimpleMovieSearch.Controllers
{
    public class VideosController : Controller
    {

        private readonly AppDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public VideosController(AppDBContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("Videos/List")]
        [Route("Videos/List/{category}")]
        public async Task<IActionResult> List(int? authorId, string authorName, int[] genresId, string genresName)
        {
            ViewBag.Title = "Videos";

            var authors = await _db.Author.ToListAsync();
            var genres = await _db.Genre.ToListAsync();

            IQueryable<Video> videos = _db.Video.Include(p => p.Author).Include(x => x.Genres);

            var genreIds = await videos.SelectMany(x => x.Genres)
                .Where(x => genresId.Contains(x.Id))
                .Select(x => x.Id)
                .Distinct()
                .ToListAsync();

            if ((authorId != null && authorId != 0) || (genresId != null && genresId.Length != 0))
            {
                if (authorId != null && authorId != 0)
                    videos = videos.Where(p => p.AuthorId == authorId);

                if (genresId != null && genresId.Length != 0)
                    videos = videos.Where(p => genreIds.Contains(p.Id));

                if ((authorId != null && authorId != 0) && (genresId != null && genresId.Length != 0))
                    videos = videos.Where(x => x.AuthorId == authorId).Where(p => genreIds.Contains(p.Id));
            }

            genres.Insert(0, new Genre { Name = "Все", Id = 0 });
            authors.Insert(0, new Author { Name = "Все", Id = 0 });

            var videoListViewModel = new VideoListViewModel
            {
                Videos = videos,
                Authors = new SelectList(authors, "Id", "Name"),
                AuthorName = authorName,
                Genres = new SelectList(genres, "Id", "Name")
            };

            return View(videoListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var video = _db.Video.Include(p => p.Genres).Include(c => c.Author).FirstOrDefault(x => x.Id == id);
            var authors = await _db.Author.ToListAsync();
            var genres = await _db.Genre.ToListAsync();

            if (id == 0)
                return View(new CreateVideoViewModel() { Authors = authors, Genres = genres });

            if (video == null)
            {
                return NotFound();
            }

            var CreateVideoViewModel = new CreateVideoViewModel
            {
                Video = video,
                Authors = authors,
                Genres = genres
            };

            return View(CreateVideoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Image,Title,ShortDescription,LongDescription,IsFavorites,AuthorId, Genres")] Video video, int[] genreIds)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    foreach (var genreId in genreIds)
                    {
                        var addGenre = await _db.Genre.FindAsync(genreId);
                        video.Genres.Add(addGenre);
                    }

                    _db.Add(video);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _db.UpdateRange(video);

                        _db.Video.Include(x => x.Genres).FirstOrDefault(g => g.Id == video.Id).Genres.Clear();

                        foreach (var genreId in genreIds)
                        {
                            var addGenre = await _db.Genre.FindAsync(genreId);
                            video.Genres.Add(addGenre);
                        }

                        await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VideoExists(video.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var video = await _db.Video.FindAsync(id);

            if (video == null)
                return NotFound();

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var video = await _db.Video.FindAsync(id);

            _db.Video.Remove(video);

            _db.SaveChanges();

            return RedirectToAction("List");
        }

        [Authorize]
        public async Task<IActionResult> AddToFavorite(int? id)
        {

            if (id == null)
                return NotFound();

            var video = await _db.Video.FindAsync(id);

            return View(video);
        }

        [HttpPost, ActionName("AddToFavorite")]
        public async Task<IActionResult> AddToFavoriteConfirmed(int id)
        {
            //var video =  _db.Video.Include(x => x.Users).FirstOrDefault(x => x.Id == id);

            var userId = _userManager.GetUserId(User);

            //var favoriteUsersVideos = new FavoriteUserVideos { UserId = userId, VideoId = id };

            _db.Add(new FavoriteUserVideos
            {
                UserId = userId,
                VideoId = id
            });

            await _db.SaveChangesAsync();

            return RedirectToAction("List");
        }

        private bool VideoExists(int id)
        {
            return _db.Video.Any(e => e.Id == id);
        }
    }
}
