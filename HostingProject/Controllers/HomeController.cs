using HostingProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HostingProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProjDbContext db;

        public HomeController(ILogger<HomeController> logger, ProjDbContext _db)
        {
            _logger = logger;
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hosting()
        {
            return View(db.Plans);
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
