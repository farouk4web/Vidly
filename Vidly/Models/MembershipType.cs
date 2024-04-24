using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MembershipType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Sign Up Fee")]
        public int SignUpFee { get; set; }

        [Required]
        [Display(Name = "Duration In Months")]
        public int DurationInMonths { get; set; }

        [Required]
        public byte Discount { get; set; }

    }
}