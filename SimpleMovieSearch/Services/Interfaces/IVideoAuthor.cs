using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Services.Interfaces
{
    public interface IVideoAuthor
    {
        IEnumerable<Author> AllAuthors { get; }
    }
}
