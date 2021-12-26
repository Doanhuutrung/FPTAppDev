using FPTAppDev.Models;
using FPTAppDev.Utils;
using FPTAppDev.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        //GET: CreateTrainee
        [HttpGet]
        public ActionResult CreateTrainee()
        {
            return View();
        }

        //POST: CreateTrainee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrainee(CreateTraineeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var TraineeId = user.Id;
                var newTrainee = new Trainee()
                {
                    TraineeId = TraineeId,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    BirthDate = viewModel.BirthDate,
                    Address = viewModel.Address,
                    Education = viewModel.Education
                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainee);
                    _context.TraineeDbset.Add(newTrainee);
                    _context.SaveChanges();
                    return RedirectToAction("TraineeList", "Staff");
                }
                AddErrors(result);
            }

            return View(viewModel);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //GET: DeleteTrainee
        [HttpGet]
        public ActionResult DeleteTrainee(string id)
        {
            var traineeInDb = _context.Users
                .SingleOrDefault(t => t.Id == id);
            var traineInDbset = _context.TraineeDbset
                .SingleOrDefault(t => t.TraineeId == id);
            if (traineeInDb == null || traineInDbset == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(traineeInDb);
            _context.TraineeDbset.Remove(traineInDbset);
            _context.SaveChanges();
            return RedirectToAction("TraineeList", "Staff");
        }

        //GET: EditTrainee
        [HttpGet]
        public ActionResult EditTrainee(string id)
        {
            var traineeInDb = _context.TraineeDbset
                .SingleOrDefault(t => t.TraineeId == id);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            return View(traineeInDb);
        }

        //POST: EditTrainee
        [HttpPost]
        public ActionResult EditTrainee(Trainee trainee)
        {
            var traineeInDb = _context.TraineeDbset.SingleOrDefault(t => t.TraineeId == trainee.TraineeId);
            if (traineeInDb == null)
            {
                return HttpNotFound();
            }
            traineeInDb.Name = trainee.Name;
            traineeInDb.Age = trainee.Age;
            traineeInDb.Address = trainee.Address;
            traineeInDb.BirthDate = trainee.BirthDate;
            traineeInDb.Education = trainee.Education;
            _context.SaveChanges();
            return RedirectToAction("TraineeList", "Staff");
        }

        //CategoryList
        public ActionResult CategoryList(string searchString)
        {
            var Category = _context.CategoryDbset
              .OrderBy(m => m.Id)
              .ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                Category = Category
                    .Where(t => t.Name.ToLower().Contains(searchString.ToLower())).
                    ToList();
            }
            return View(Category);
        }

        //GET: CreateCategory
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        //POST: CreateCategory
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description
            };
            try
            {
                _context.CategoryDbset.Add(newCategory);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("duplicate", "Category name already existed");
                return View(category);
            }

            return RedirectToAction("CategoryList", "Staff");
        }

        //GET: DeleteCategory
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var categoryInDb = _context.CategoryDbset.SingleOrDefault(t => t.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            _context.CategoryDbset.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("CategoryList", "Staff");
        }

        //GET: EditCategory
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryInDb = _context.CategoryDbset.SingleOrDefault(t => t.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);
        }

        //POST: EditCategory
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            var categoryInDb = _context.CategoryDbset.SingleOrDefault(t => t.Id == category.Id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ModelState.AddModelError("duplicate", "Category name already existed");
                return View(category);
            }
            return RedirectToAction("CategoryList", "Staff");
        }
    }
}