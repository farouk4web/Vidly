using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;
using System.IO;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var movies = _context.Movies.
                Include(c => c.Genre).
                ToList();

            return View(movies);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).
                Single(m => m.Id == id);
            return PartialView("_Details", movie);
        }

        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel
            {
                PageTitle = "New Movie",
                Movie = new Movie(),
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);            
        }

        public ActionResult Update(int id)
        {
            var movie = _context.Movies.Single(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                PageTitle = "Edit Movie",
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie, HttpPostedFileBase MovieImage)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            else
            {
                if (movie.Id == 0)  // new movie
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads/Movies"), MovieImage.FileName);
                    MovieImage.SaveAs(path);
                    movie.ImageSrc = MovieImage.FileName;

                    movie.DateAdded = DateTime.Now;
                    _context.Movies.Add(movie);
                }
                else
                {
                    var movieInDB = _context.Movies.Single(c => c.Id == movie.Id);
                    movieInDB.Name = movie.Name;
                    movieInDB.Language = movie.Language;
                    movieInDB.NumberInStock = movie.NumberInStock;
                    movieInDB.Price = movie.Price;
                    movieInDB.GenreId = movie.GenreId;
                    movieInDB.ReleaseDate = movie.ReleaseDate;
                    movieInDB.RunningTime = movie.RunningTime;
                    movieInDB.MoviePageOnImdb = movie.MoviePageOnImdb;
                    movieInDB.MovieFileSrc = movie.MovieFileSrc;

                    if (MovieImage == null)
                    {
                    }
                    else
                    {
                        string path = Path.Combine(Server.MapPath("~/Uploads/Movies"), MovieImage.FileName);
                        MovieImage.SaveAs(path);
                        movie.ImageSrc = MovieImage.FileName;
                        movieInDB.ImageSrc = movie.ImageSrc;
                    }
                   

                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}

