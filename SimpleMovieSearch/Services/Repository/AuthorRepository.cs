using SimpleMovieSearch.Data;
using SimpleMovieSearch.Models;
using SimpleMovieSearch.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Services.Repository
{
    public class AuthorRepository : IVideoAuthor
    {
        private readonly AppDBContent appDBContent;

        public AuthorRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Author> AllAuthors => appDBContent.Author;
    }
}
