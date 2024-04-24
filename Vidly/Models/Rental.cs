using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public Movie Movie { get; set; }

        [Required]
        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        [Display(Name = "Date Rented")]
        public DateTime DateRented { get; set; }

        [Display(Name = "Date Returned")]
        [DataType(DataType.Date)]
        public DateTime? DateReturned { get; set; }

        public bool Returned { get; set; }
    }
}