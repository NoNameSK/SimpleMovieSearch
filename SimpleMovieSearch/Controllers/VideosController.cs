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

        private readonly AppDBContent _content;

        public VideosController(AppDBContent content)
        {
            _content = content;
        }

        [Route("Videos/List")]
        [Route("Videos/List/{category}")]
        public async Task<IActionResult> List(int? authorId, string authorName, int[] genresId, string genresName)
        {
            ViewBag.Title = "Videos";

            var videoIds = await _content.VideoGenres
                .Where(x => genresId.Contains(x.GenresId)) 
                .Select(x => x.VideosId)
                .Distinct()
                .ToListAsync();

            var authors = await _content.Author.ToListAsync();
            var genres = await _content.Genre.ToListAsync();

            IQueryable <Video> videos = _content.Video.Include(p => p.Author).Include(x => x.Genres);
            if ((authorId != null && authorId != 0) || (genresId != null && genresId.Length != 0))
            {
                if (authorId != null && authorId != 0)
                    videos = videos
                        .Where(p => p.AuthorId == authorId);

                else if (genresId != null && genresId.Length != 0)
                    videos = videos
                    .Where(x => videoIds.Contains(x.Id));

                else 
                    videos = videos
                        .Where(x => x.AuthorId == authorId)
                        .Where(x => videoIds.Contains(x.Id));
            }

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
                VideoGenres = _content.VideoGenres.ToList()
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
                    _content.Add(video);
                    await _content.SaveChangesAsync();

                    foreach (var ids in genreIds)
                    {
                        var videoGenres = new VideoGenres { VideosId = _content.Video.OrderBy(x => x.Id).Last().Id };

                        videoGenres.GenresId = ids;
                        _content.VideoGenres.Add(videoGenres);
                    }

                    _content.SaveChanges();
                }
                else
                {
                    try
                    {
                        _content.Update(video);
                        await _content.SaveChangesAsync();

                        var videoGenresForRemovie = _content.VideoGenres.ToList().FindAll(x => x.VideosId == video.Id);

                        _content.VideoGenres.RemoveRange(videoGenresForRemovie);
                        _content.SaveChanges();

                        foreach (var ids in genreIds)
                        {
                            var videoGenres = new VideoGenres { VideosId = video.Id };

                            videoGenres.GenresId = ids;
                            _content.VideoGenres.Add(videoGenres);
                        }
                        _content.SaveChanges();
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

            var videoGenresForRemovie = _content.VideoGenres.ToList().FindAll(x => x.VideosId == video.Id);

            _content.VideoGenres.RemoveRange(videoGenresForRemovie);
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
