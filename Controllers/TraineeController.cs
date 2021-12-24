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
    }
}