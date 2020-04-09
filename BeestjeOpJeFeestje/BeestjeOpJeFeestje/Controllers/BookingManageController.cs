using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingManageController : Controller
    {
        private IBookingRepository dbBooking;
        private ICustomerRepository dbCustomer;

        public BookingManageController(IBookingRepository db1, ICustomerRepository db2)
        {
            this.dbBooking = db1;
            this.dbCustomer = db2;
        }

        // GET: BookingManage
        public ActionResult Index()
        {
            return View(dbBooking.GetBookings().ToList().Where(b => b.IsConfirmed != null));
        }

        // GET: BookingManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = dbBooking.FindById(Convert.ToInt32(id));
            if (booking == null)
            {
                return HttpNotFound();
            }

            Customer person = null;
            var customers = dbCustomer.GetCustomers().ToList();

            foreach(var customer in customers)
            {
                if (customer.Bookings.Select(b => b.Id).Contains(booking.Id))
                {
                    person = customer;
                }
            }

            booking.customer = person;

            return View(booking);
        }

        // GET: BookingManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = dbBooking.FindById(Convert.ToInt32(id));
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: BookingManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbBooking.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
