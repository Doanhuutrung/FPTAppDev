using FPTAppDev;
using FPTAppDev.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTAppDev.Controllers
{
    public class TraineeController : Controller
    {
        private ApplicationDbContext _context;
        public TraineeController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Trainee
        public ActionResult Index()
        {
            return View();
        }
        // TraineeInfo
        public ActionResult TraineeInfo()
        {
            var userId = User.Identity.GetUserId();
            var traineeInDb = _context.TraineeDbset
                .SingleOrDefault(t => t.TraineeId == userId);
            return View(traineeInDb);
        }
        //GET: TraineeCourse
        [HttpGet]
        public ActionResult TraineeCourse()
        {
            var userId = User.Identity.GetUserId();
            var catagory = _context.CategoryDbset.ToList();
            var courses = _context.TraineeCourseDbset
                .Where(t => t.Trainee.TraineeId == userId)
                .Select(t => t.Course)
                .ToList();
            return View(courses);
        }
        //GET: ViewTrainee
        [HttpGet]
        public ActionResult ViewTrainee(int id)
        {
            var user = _context.Users.ToList();
            var traineesCourse = _context.TraineeCourseDbset
                .Where(t => t.CourseId == id)
                .Select(t => t.Trainee)
                .ToList();
            return View(traineesCourse);
        }
    }
}