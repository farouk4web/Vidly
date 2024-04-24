using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var rentals = _context.Rentals.
                Include(m => m.Movie).
                Include(m => m.Customer).
                ToList();
            return View(rentals);
        }

        public ActionResult New()
        {
            NewRentalViewModel viewModel = new NewRentalViewModel
            {
                Customers = _context.Customers.ToList(),
                Movies = _context.Movies.ToList(),
                Rental = new Rental()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Rental rental)
        {
            if (!ModelState.IsValid)
                return View(rental);

            rental.DateRented = DateTime.Now;

            var movieRented =_context.Movies.Single(m => m.Id == rental.MovieId);
            movieRented.NumberInStock--;

            _context.Rentals.Add(rental);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ReturnMovies(int id)
        {
            var rental = _context.Rentals.Single(m => m.Id == id);
            var rentedMovie = _context.Movies.Single(m => m.Id == rental.MovieId);

            if (rental.Returned == false)
            {
                rentedMovie.NumberInStock++;
                rental.Returned = true;
                _context.SaveChanges();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var rental = _context.Rentals.Single(m => m.Id == id);
            if (rental == null)
                return HttpNotFound();

            _context.Rentals.Remove(rental);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}