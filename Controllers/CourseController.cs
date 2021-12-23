using FPTAppDev.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using FPTAppDev.ViewModel;

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

        //GET: CreateCourse
        [HttpGet]
        public ActionResult CreateCourse()
        {
            var category = _context.CategoryDbset.ToList();
            var viewModel = new CreateCourseViewModel()
            {
                Category = category,
            };
            return View(viewModel);
        }
        //POST: CreateCourse
        [HttpPost]
        public ActionResult CreateCourse(CreateCourseViewModel model)
        {
            var newCourse = new Course()
            {
                Name = model.Course.Name,
                Description = model.Course.Description,
                CategoryId = model.Course.CategoryId,
            };
            _context.CourseDbset.Add(newCourse);
            _context.SaveChanges();
            return RedirectToAction("CourseList", "Course");
        }

        //GET: DeleteCourse
        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            var CourseInDb = _context.CourseDbset.SingleOrDefault(t => t.Id == id);
            if (CourseInDb == null)
            {
                return HttpNotFound();
            }
            _context.CourseDbset.Remove(CourseInDb);
            _context.SaveChanges();
            //Reseed the identity to 0.
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Courses, RESEED, 0)");
            //Roll the identity forward till it finds the last used number.
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Courses, RESEED)");
            return RedirectToAction("CourseList", "Course");
        }

        //GET: EditCourse
        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var CourseInDb = _context.CourseDbset.SingleOrDefault(t => t.Id == id);
            if (CourseInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CreateCourseViewModel
            {
                Course = CourseInDb,
                Category = _context.CategoryDbset.ToList()
            };
            return View(viewModel);
        }
        //POST: EditCourse
        [HttpPost]
        public ActionResult EditCourse(CreateCourseViewModel model)
        {
            var CourseInDb = _context.CourseDbset.SingleOrDefault(t => t.Id == model.Course.Id);
            if (CourseInDb == null)
            {
                return HttpNotFound();
            }
            CourseInDb.Name = model.Course.Name;
            CourseInDb.Description = model.Course.Description;
            CourseInDb.CategoryId = model.Course.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("CourseList", "Course");
        }

        //GET: TrainerIncourse
        [HttpGet]
        public ActionResult TrainerInCourse(string searchString)
        {
            List<TrainerCourseViewModel> viewModel = _context.TrainerCourseDbset
                .GroupBy(i => i.Course)
                .Select(res => new TrainerCourseViewModel
                {
                    Course = res.Key,
                    Trainers = res.Select(u => u.Trainer).ToList()
                })
                .ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                viewModel = viewModel
                    .Where(t => t.Course.Name.ToLower().Contains(searchString.ToLower())).
                    ToList();
            }
            return View(viewModel);
        }

        //GET: AssignTrainer
        [HttpGet]
        public ActionResult AssignTrainer()
        {
            var viewModel = new TrainerCourseViewModel
            {
                Courses = _context.CourseDbset.ToList(),
                Trainers = _context.TrainerDbset.ToList()
            };
            return View(viewModel);
        }
        //POST: AssignTrainer
        [HttpPost]
        public ActionResult AssignTrainer(TrainerCourseViewModel viewModel)
        {
            var model = new TrainerCourse
            {
                CourseId = viewModel.CourseId,
                TrainerId = viewModel.TrainerId
            };

            List<TrainerCourse> trainerCourse = _context.TrainerCourseDbset.ToList();
            bool alreadyExist = trainerCourse.Any(i => i.CourseId == model.CourseId && i.TrainerId == model.TrainerId);
            if (alreadyExist == true)
            {
                return RedirectToAction("TrainerInCourse", "Course");
            }
            _context.TrainerCourseDbset.Add(model);
            _context.SaveChanges();
            return RedirectToAction("TrainerInCourse", "Course");
        }

        //GET: RemoveTrainer
        [HttpGet]
        public ActionResult RemoveTrainer()
        {
            var trainers = _context.TrainerCourseDbset.Select(t => t.Trainer)
                .Distinct()
                .ToList();
            var courses = _context.TrainerCourseDbset.Select(t => t.Course)
                .Distinct()
                .ToList();

            var viewModel = new TrainerCourseViewModel
            {
                Courses = courses,
                Trainers = trainers
            };
            return View(viewModel);
        }
        //POST: RemoveTrainer
        [HttpPost]
        public ActionResult RemoveTrainer(TrainerCourseViewModel viewModel)
        {
            var trainer = _context.TrainerCourseDbset
                .SingleOrDefault(t => t.CourseId == viewModel.CourseId && t.TrainerId == viewModel.TrainerId);
            if (trainer == null)
            {
                return RedirectToAction("TrainerInCourse", "Course");
            }

            _context.TrainerCourseDbset.Remove(trainer);
            _context.SaveChanges();

            return RedirectToAction("TrainerInCourse", "Course");
        }
    }
}