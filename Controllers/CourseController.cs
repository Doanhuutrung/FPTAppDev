using FPTAppDev.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace FPTAppDev.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public CourseController()
        {
            _context = new ApplicationDbContext();
        }
        public CourseController(ApplicationUserManager userManager)
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
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        //CourseList
        public ActionResult CourseList(string searchString)
        {
            var Course = _context.CourseDbset
                  .Include(t => t.Category)
                  .ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                Course = Course
                    .Where(t => t.Name.ToLower().Contains(searchString.ToLower())).
                    ToList();
            }
            return View(Course);
        }

    }
}