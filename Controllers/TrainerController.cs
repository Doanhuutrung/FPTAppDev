using FPTAppDev.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace FPTAppDev.Controllers
{
    public class TrainerController : Controller
    {
        private ApplicationDbContext _context;
        public TrainerController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrainerInfo()
        {
            var userId = User.Identity.GetUserId();
            var trainerInDb = _context.TrainerDbset
                .SingleOrDefault(t => t.TrainerId == userId);
            return View(trainerInDb);
        }
        //GET: Edit
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var trainerInDb = _context.TrainerDbset
                .SingleOrDefault(t => t.TrainerId == id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }
        //POST: Edit
        [HttpPost]
        public ActionResult Edit(Trainer trainer)
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
            return RedirectToAction("TrainerInfo", "Trainer");
        }
    }
