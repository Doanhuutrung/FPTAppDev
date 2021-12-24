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
