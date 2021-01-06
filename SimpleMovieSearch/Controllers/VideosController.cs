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

namespace SimpleMovieSearch.Controllers
{
    public class VideosController : Controller
    {

        private readonly AppDBContext _content;

        public VideosController(AppDBContext content)
        {
            _content = content;
        }

        [Route("Videos/List")]
        [Route("Videos/List/{category}")]
        public async Task<IActionResult> List(int? authorId, string authorName, int[] genresId, string genresName)
        {
            ViewBag.Title = "Videos";

            IQueryable<Video> videos = _content.Video.Include(p => p.Author).Include(x => x.Genres);

            var genreIds =  videos.SelectMany(x => x.Genres)
                .Where(x => genresId.Contains(x.Id))
                .Select(x => x.Id)
                .Distinct()
                .ToList();

            var authors = await _content.Author.ToListAsync();
            var genres = await _content.Genre.ToListAsync();

            if (authorId != null && authorId != 0)
                 videos.Where(p => p.AuthorId == authorId);

            if (genresId != null && genresId.Length != 0)
                 videos.Where(x => genreIds.Contains(x.Id));

            if ((authorId != null && authorId != 0) && (genresId != null && genresId.Length != 0))
                 videos.Where(x => x.AuthorId == authorId).Where(x => genreIds.Contains(x.Id));

            genres.Insert(0, new Genre { Name = "Все", Id = 0 });

            authors.Insert(0, new Author { Name = "Все", Id = 0 });

            var videoListViewModel = new VideoListViewModel
            {
                Videos = videos.ToList(),
                Authors = new SelectList(authors, "Id", "Name"),
                AuthorName = authorName,
                Genres = new SelectList(genres, "Id", "Name")
            };

            return View(videoListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var authors = _content.Author.ToList();
            var genres = _content.Genre.Include(x => x.Videos).ToList();

            if (id == 0)
                return View(new CreateVideoViewModel() { Authors = authors, Genres = genres });

            var video = await _content.Video.Include(p => p.Genres).FirstOrDefaultAsync(x => x.Id == id);

            if (video == null)
            {
                return NotFound();
            }
            var CreateVideoViewModel = new CreateVideoViewModel
            {
                Video = video,
                Authors = authors,
                Genres = genres,
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
                        var addGenre = await _content.Genre.FindAsync(genreId);
                        video.Genres.Add(addGenre);
                    }

                    _content.Add(video);
                    await _content.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _content.UpdateRange(video);

                        _content.Video.Include(x => x.Genres).FirstOrDefault(g => g.Id == video.Id).Genres.Clear();

                        foreach (var genreId in genreIds)
                        {
                            var addGenre = await _content.Genre.FindAsync(genreId);
                            video.Genres.Add(addGenre);
                        }

                        await _content.SaveChangesAsync();
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

            var video = await _content.Video.FindAsync(id);

            if (video == null)
                return NotFound();

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _content.Video.FindAsync(id);

            _content.Video.Remove(video);

            _content.SaveChanges();

            return RedirectToAction("List");
        }

        public async Task<IActionResult> AddToFavorite(int? id)
        {

            if (id == null)
                return NotFound();

            var video = await _content.Video.FindAsync(id);
            return View(video);

            if (video == null)
                return NotFound();
        }

        [HttpPost, ActionName("AddToFavorite")]
        public async Task<IActionResult> AddToFavoriteConfirmed(int id)
        {
            var video = await _content.Video.FindAsync(id);

            _content.User.FirstOrDefault(x => x.Email == User.Identity.Name).FavoriteVideos.Add(video);
            _content.SaveChanges();

            return RedirectToAction("List");
        }

        private bool VideoExists(int id)
        {
            return _content.Video.Any(e => e.Id == id);
        }
    }
}
