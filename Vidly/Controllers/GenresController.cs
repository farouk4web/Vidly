using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Genres
        [AllowAnonymous]
        public ActionResult Index()
        {
            var genres = _context.Genres.ToList();
            return View(genres);
        }

        // New Genre
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Genre genre)
        {
            if(!ModelState.IsValid)
                return View("New", genre);

            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}