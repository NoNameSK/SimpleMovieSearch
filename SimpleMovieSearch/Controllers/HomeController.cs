using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleMovieSearch.Data;
using SimpleMovieSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.AppContext _content;
        public HomeController(Data.AppContext content)
        {
            _content = content;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Title = User.Identity.Name.ToString();

            return View();
        }
    }
}
