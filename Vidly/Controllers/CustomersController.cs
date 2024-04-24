using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;
using System.IO;

namespace Vidly.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // Customers section
        public ActionResult Index(string q)
        {
            var customers = _context.Customers
                .Include(c => c.MembershipType)
                .ToList();

            if (!string.IsNullOrEmpty(q))
            {
               customers = _context.Customers
              .Where(c => c.Name.StartsWith(q))
              .Include(c => c.MembershipType)
              .ToList();
            }
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).Single(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return PartialView("_Details", customer);
        }

        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                PageTitle = "New Customer",
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Update(int id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                PageTitle = "Update Customer",
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer, HttpPostedFileBase personalPicture)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads/Customers"), personalPicture.FileName);
                personalPicture.SaveAs(path);

                customer.PersonalPictureSrc = personalPicture.FileName;
                _context.Customers.Add(customer);
            }

            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.SupscripeToNewsletter = customer.SupscripeToNewsletter;

                if (personalPicture == null)
                {
                }
                else
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads/Customers"), personalPicture.FileName);
                    personalPicture.SaveAs(path);
                    customer.PersonalPictureSrc = personalPicture.FileName;
                    customerInDb.PersonalPictureSrc= customer.PersonalPictureSrc;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(int id)
        {
            var customer = _context.Customers.Single(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        // Membershiptypes section
        [AllowAnonymous]
        public ActionResult MembershipTypes()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            return View(membershipTypes);
        }

        public ActionResult NewMembershipType()
        {
            return View("MembershipTypeForm");
        }
        
        public ActionResult UpdateMembershipType(int id)
        {
            var membershipType = _context.MembershipTypes.Single(c => c.Id == id);
            if (membershipType == null)
                return HttpNotFound();
            return View("MembershipTypeForm", membershipType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMembershipType(MembershipType membershipType)
        {
            if (!ModelState.IsValid)
                return View("MembershipTypeForm", membershipType);
            if (membershipType.Id == 0)
            {
                _context.MembershipTypes.Add(membershipType);
            }
            else
            {
                var membershipTypeInDb = _context.MembershipTypes.Single(c => c.Id == membershipType.Id);
                membershipTypeInDb.Name = membershipType.Name;
                membershipTypeInDb.SignUpFee = membershipType.SignUpFee;
                membershipTypeInDb.DurationInMonths = membershipType.DurationInMonths;
                membershipTypeInDb.Discount = membershipType.Discount;
            }
            _context.SaveChanges();
            return RedirectToAction("MembershipTypes");
        }

        [HttpPost]
        public ActionResult RemoveMembershipType(int id)
        {
            var membershipType = _context.MembershipTypes.Single(c => c.Id == id);

            if (membershipType == null)
                return HttpNotFound();

            _context.MembershipTypes.Remove(membershipType);
            _context.SaveChanges();

            return RedirectToAction("MembershipTypes");
        }
    }
}