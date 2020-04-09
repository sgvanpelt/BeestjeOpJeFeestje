using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeestjeOpJeFeestje.Controllers
{
    public class HomeController : Controller
    {
        private IBookingRepository db;
        public HomeController(IBookingRepository db)
        {
            this.db = db;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBooking([Bind(Include = "Id,Date,Payment,Customer_Id")] Booking booking)
        {
            if (CheckDate(booking) == true)
            {
                db.Add(booking);
                return RedirectToAction("AnimalsBooking", "Bookings", booking);
            }
            ViewBag.ErrorMessage = "Datum moet in de toekomst liggen.";
            return View("Index");
        }

        private bool CheckDate(Booking booking)
        {
            var customer = booking.Date;
            if (booking.Date < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}