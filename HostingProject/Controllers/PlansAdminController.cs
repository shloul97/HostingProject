using HostingProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostingProject.Controllers
{
    
    public class PlansAdminController : Controller
    {
        private ProjDbContext db;

        public PlansAdminController(ProjDbContext _db) 
        {
            db= _db;
        }
        public IActionResult Index()
        {
            return View(db.Plans);
        }

        public IActionResult AddPlan() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlan(PlanViewModel plan)
        {
            
            db.Plans.Add(plan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditPlan(PlanViewModel plan)
        {
            if (plan == null)
            {
                return RedirectToAction("Index");
            }
            else 
            {
                db.Plans.Update(plan);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(plan);
        }


        [HttpGet]
        public IActionResult EditPlan(int id)
        {
            var plan = db.Plans.Find(id);
            if (plan == null)
            {
                return RedirectToAction("Index");
            }
            return View(plan);
        }

    }
}
