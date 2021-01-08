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
        private readonly Data.AppDBContext _db;
        public HomeController(Data.AppDBContext сontext)
        {
            _db = сontext;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.Title = User.Identity.Name.ToString();

            return View();
        }
    }
}
