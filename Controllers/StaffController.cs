using FPTAppDev.Models;
using FPTAppDev.Utils;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace FPTAppDev.Controllers
{
    [Authorize(Roles = Role.Staff)]
    public class StaffController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public StaffController()
        {
            _context = new ApplicationDbContext();
        }
        public StaffController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            _context = new ApplicationDbContext();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        //Trainee List
        public ActionResult TraineeList(string searchString)
        {
            var Trainee = _context.TraineeDbset.Include(t => t.User).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                Trainee = Trainee.Where(t => t.Name.ToLower().Contains(searchString.ToLower()) 
                || t.Age.ToString().Contains(searchString.ToLower()))
                .ToList();
            }
            return View(Trainee);
        }

    }
}