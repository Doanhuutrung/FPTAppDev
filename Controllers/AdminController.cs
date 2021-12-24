using FPTAppDev.Models;
using FPTAppDev.Utils;
using FPTAppDev.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static FPTAppDev.Controllers.ManageController;

namespace FPTAppDev.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        public AdminController(ApplicationUserManager userManager)
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
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        //Staff List
        public ActionResult StaffList()
        {
            var Staff = _context.StaffDbset.Include(t => t.User).ToList();
            return View(Staff);
        }
        //GET: CreateStaff
        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
        }
        //POST: CreateStaff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStaff(CreateStaffViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var StaffId = user.Id;
                var newStaff = new Staff()
                {
                    StaffId = StaffId,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    Address = viewModel.Address
                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Staff);
                    _context.StaffDbset.Add(newStaff);
                    _context.SaveChanges();
                    return RedirectToAction("StaffList", "Admin");
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

        //GET: DeleteStaff
        [HttpGet]
        public ActionResult DeleteStaff(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var staffInDbset = _context.StaffDbset.SingleOrDefault(t => t.StaffId == id);
            if (staffInDb == null || staffInDbset == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(staffInDb);
            _context.StaffDbset.Remove(staffInDbset);
            _context.SaveChanges();
            return RedirectToAction("StaffList", "Admin");
        }

        //GET: EditStaff
        [HttpGet]
        public ActionResult EditStaff(string id)
        {
            var staffInDb = _context.StaffDbset.SingleOrDefault(t => t.StaffId == id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            return View(staffInDb);
        }
        //POST: EditStaff
        [HttpPost]
        public ActionResult EditStaff(Staff staff)
        {
            var staffInDb = _context.StaffDbset.SingleOrDefault(t => t.StaffId == staff.StaffId);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            staffInDb.Name = staff.Name;
            staffInDb.Age = staff.Age;
            staffInDb.Address = staff.Address;

            _context.SaveChanges();
            return RedirectToAction("StaffList", "Admin");
        }

        //GET: StaffChangePassword
        [HttpGet]
        public ActionResult StaffChangePassword()
        {
            return View();
        }
        //POST: StaffChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StaffChangePassword(ChangePassViewModel model, string id)
        {
            var userInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (userInDb == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = userInDb.Id;

            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassword = model.NewPassword;
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("StaffList", "Admin", new { Message = ManageMessageId.ChangePasswordSuccess });
        }

        //Trainer List
        public ActionResult TrainerList()
        {
            var Trainer = _context.TrainerDbset.Include(t => t.User).ToList();
            return View(Trainer);
        }

        //GET: CreateTrainer
        [HttpGet]
        public ActionResult CreateTrainer()
        {
            return View();
        }
        //POST: CreateTrainer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrainer(CreateTrainerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var TrainerId = user.Id;
                var newTrainer = new Trainer()
                {
                    TrainerId = TrainerId,
                    Name = viewModel.Name,
                    Age = viewModel.Age,
                    Address = viewModel.Address,
                    Specialty = viewModel.Specialty
                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainer);
                    _context.TrainerDbset.Add(newTrainer);
                    _context.SaveChanges();
                    return RedirectToAction("TrainerList", "Admin");
                }
                AddErrors(result);
            }

            return View(viewModel);
        }

        //GET: DeleteTrainer
        [HttpGet]
        public ActionResult DeleteTrainer(string id)
        {
            var trainerInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var trainerInDbset = _context.TrainerDbset.SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null || trainerInDbset == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(trainerInDb);
            _context.TrainerDbset.Remove(trainerInDbset);
            _context.SaveChanges();
            return RedirectToAction("TrainerList", "Admin");
        }

        //GET: EditTrainer
        [HttpGet]
        public ActionResult EditTrainer(string id)
        {
            var trainerInDb = _context.TrainerDbset.SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }
        //POST: EditTrainer
        [HttpPost]
        public ActionResult EditTrainer(Trainer trainer)
        {
            var trainerInDb = _context.TrainerDbset.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            trainerInDb.Name = trainer.Name;
            trainerInDb.Age = trainer.Age;
            trainerInDb.Address = trainer.Address;
            trainerInDb.Specialty = trainer.Specialty;

            _context.SaveChanges();
            return RedirectToAction("TrainerList", "Admin");
        }

        //GET: StaffChangePassword
        [HttpGet]
        public ActionResult TrainerChangePassword()
        {
            return View();
        }
        //POST: StaffChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrainerChangePassword(ChangePassViewModel model, string id)
        {
            var userInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (userInDb == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = userInDb.Id;

            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassword = model.NewPassword;
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("TrainerList", "Admin", new { Message = ManageMessageId.ChangePasswordSuccess });
        }

    }
}