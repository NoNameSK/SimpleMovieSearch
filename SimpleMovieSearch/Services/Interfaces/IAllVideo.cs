using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Services.Interfaces
{
    public interface IAllVideo
    {
        IEnumerable<Video> Videos { get; }
        IEnumerable<Video> GetFavoriteVideos { get; }
        Video GetObjectVideo(int videoID);
    }
}
