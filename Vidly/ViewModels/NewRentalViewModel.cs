using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewRentalViewModel
    {
        public Rental Rental { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}