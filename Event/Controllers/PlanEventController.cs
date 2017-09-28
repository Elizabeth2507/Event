﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Event.DAL;
using Event.Models;
using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Event.Controllers
{
    public class PlanEventController: Controller
    {
        private PlannerContext db = new PlannerContext();

        public ViewResult Index()
        {
            return View(db.PlanEvents.ToList());
        }

        // GET: Movies/Create 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, StartDateTime, Location, Description, MaxCountGuest")]PlanEvent planEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PlanEvents.Add(planEvent);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(planEvent);
        }
        public ActionResult Activation()
        {
            ViewBag.Message = "Invalid Activation code.";
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());
                
                
                Guest guest = db.Guests.Where(p => p.ActivationCode == activationCode).FirstOrDefault();
                if (guest != null)
                {
                    db.Guests.Remove(guest);
                    db.SaveChanges();
                    ViewBag.Message = "Activation successful.";
                }
            }

            return View();
        }

        // GET: Movies/Create 
        public ActionResult Letter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Letter(EmailForm model)
        {
            Guid activationCode = Guid.NewGuid();
            db.Guests.Add(new Guest
            {
                Email = model.ToEmail,
                ActivationCode = activationCode
            });
            db.SaveChanges();    
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + string.Format("{0}://{1}/PlanEvent/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("elizabeth.olamisan@gmail.com")); //replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }

}