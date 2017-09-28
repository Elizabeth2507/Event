using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Event.DAL;
using System.Threading.Tasks;
using System.Web.Mvc;
using Event.Models;
using System.Net;
using System.Data.Entity;
using System.Data;

namespace Event.Controllers
{
    public class GuestController: Controller
    {
        private PlannerContext db = new PlannerContext();

        // GET: Guest/Details/5
        [HttpGet]
        public ActionResult Details()
        {
            int? arg = Convert.ToInt32(TempData["arg"]);

            if (arg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var p = db.PlanEvents.FirstOrDefault(i => i.ID== arg);

            IQueryable<PlanEvent> p = from ev in db.PlanEvents
                                        where ev.ID == arg
                                        select ev;
            if (p == null)
            {
                return HttpNotFound();
            }
            
            return View(p.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName, LastName, Email")]Guest guest)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Guests.Add(guest);
                        db.SaveChanges();
                        return RedirectToAction("Confirm");
                    }
                }
                catch (DataException /* dex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
                return  View(guest);
            }
            public ActionResult Confirm()
            {
                ViewBag.Message = "Activation successful.";
                return View();
            }
        


        protected override void Dispose(bool disposing)
                {
                    db.Dispose();
                    base.Dispose(disposing);
                }

        }
}