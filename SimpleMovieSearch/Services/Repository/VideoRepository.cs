using SimpleMovieSearch.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;

namespace SimpleMovieSearch.Services.Repository
{
    public class VideoRepository : IAllVideo
    {
        private readonly AppDBContent appDBContent;

        public VideoRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Video> Videos => appDBContent.Video.Include(c => c.Author);

        public IEnumerable<Video> GetFavoriteVideos => appDBContent.Video.Where(p => p.IsFavorites).Include(c => c.Author);

        public Video GetObjectVideo(int videoID) => appDBContent.Video.FirstOrDefault(p => p.Id == videoID);
    }
}
