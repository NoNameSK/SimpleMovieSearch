using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsFavorites { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<User> Users { get; set; }
    }
}
