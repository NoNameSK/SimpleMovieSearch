using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SimpleMovieSearch.ViewModels;

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
        public async Task<IActionResult> List()
        {
            ViewBag.Title = "Videos";

            return View(await _content.Video.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var authors = _content.Author.ToList();
            var genres = _content.Genre.ToList();

            if (id == 0)
                return View(new VideoListViewModel() { Authors = authors, Genres = genres });

            var video = await _content.Video.Include(p => p.Genres).FirstOrDefaultAsync(x => x.Id == id);

            if (video == null)
            {
                return NotFound();
            }
            var videoListViewModel = new VideoListViewModel { Video = video, Authors = authors, Genres = genres };
            return View(videoListViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Image,Title,ShortDescription,LongDescription,IsFavorites,AuthorId,GenresId")] Video video)
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

        //public async Task<IActionResult> AddToFavorite(int? id)
        //{
        //    var video = await _content.Video.FindAsync(id);
        //    return View(video);

        //    //if (ModelState.IsValid)
        //    //{
        //    //    _content.User.FirstOrDefault(x => x.Email == User.Identity.Name).FavoriteVideos.Add(video);
        //    //    await _content.SaveChangesAsync();

        //    //    return RedirectToAction("FavoriteVideos/AllFavoriteVideos");
        //    //}
        //    //else
        ////    //    return RedirectToAction("List");
        //}




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
