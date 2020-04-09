using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Controllers
{
    public class BookingsController : Controller
    {
        private IBookingRepository dbBooking;
        private IAccesoryRepository dbAccessory;
        private IAnimalRepository dbAnimal;
        private ICustomerRepository dbCustomer;

        public BookingsController(IBookingRepository db1, IAccesoryRepository db2, IAnimalRepository db3, ICustomerRepository db4)
        {
            this.dbBooking = db1;
            this.dbAccessory = db2;
            this.dbAnimal = db3;
            this.dbCustomer = db4;
        }

        public void getAnimals(BookingViewModel bookVM)
        {
            bool found = false;
            foreach (Animal animals in dbAnimal.GetAnimals().ToList())
            {
                foreach (Booking booking in animals.Bookings)
                {
                    if ((booking.Date == bookVM.Booking.Date) && (booking.IsConfirmed < DateTime.Now))
                    {
                        found = true;
                        break;
                    }

                }
                if (found == true)
                {
                    found = false;
                }
                else
                {
                    bookVM.Animals.Add(animals);
                }
            }
        }

        public ActionResult AnimalsBooking(Booking book)
        {
            BookingViewModel BookVM = new BookingViewModel();

            if (book == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookVM.Booking = book;
            getAnimals(BookVM);

            return View(BookVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AccessoiresBooking(BookingViewModel bookVM)
        {
            AnimalValidationViewModel validation = new AnimalValidationViewModel();
            bookVM.Booking = dbBooking.GetBookings().Where(x => x.Id.Equals(bookVM.Booking.Id)).FirstOrDefault();

            string errors = null;
            var selectedanimals = bookVM.Animals.Where(x => x.IsChecked == true).ToList<Animal>();

            foreach (var item in selectedanimals) 
            {
                bookVM.tempAnimals.Add(item);
            }

            if (validation.CheckFarm(bookVM) == false)
            {
                errors += "Je mag geen leeuw of ijsbeer boeken als je een boerderij dier hebt.";
            }
            if (validation.CheckPinguïn(bookVM) == false)
            {
                errors +=  "Je mag geen Pinguïn in het weekend kiezen.";
            }
            if (validation.CheckDesert(bookVM) == false)
            { 
                errors +=  "Je mag geen beestje met het type woestijn in de maanden Oktober t/m Februari boeken.";
            }
            if (validation.CheckSnow(bookVM) == false)
            { 
                errors +=  "Je mag geen beestje met het type sneeuw in de maanden Juni t/m Augustus boeken.";
            }              

            if(errors != null)
            {
                bookVM.Animals.Clear();
                getAnimals(bookVM);
                ViewBag.ErrorMessage = errors;
                return View("AnimalsBooking", bookVM);
            }

            foreach (var selectedanimal in selectedanimals)
            {
                dbBooking.AddAnimalsToOrder(selectedanimal.Id, bookVM.Booking.Id);
                var animal = dbAnimal.GetAnimals().ToList().Where(a => a.Id == selectedanimal.Id).FirstOrDefault();
                bookVM.BookingAnimals.Add(animal);
            }

            foreach(var animal in dbAnimal.GetAnimals().ToList())
            {
                foreach(Booking booking in animal.Bookings)
                {
                    if (booking.Id.Equals(bookVM.Booking.Id))
                    {
                        foreach (var acc in animal.Accessories)
                        {
                            bookVM.Accessories.Add(acc);
                        }
                    }
                }
            }
            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerBooking(BookingViewModel bookVM)
        {
             bookVM.Booking = dbBooking.GetBookings().Where(x => x.Id.Equals(bookVM.Booking.Id)).FirstOrDefault();

            var selectedAccessoires = bookVM.Accessories.Where(x => x.IsChecked == true).ToList<Accessory>();
            foreach (var selectedAccessory in selectedAccessoires)
                {
                    dbBooking.AddAccessoriesToOrder(selectedAccessory.Id, bookVM.Booking.Id);
                    var accessory = dbAccessory.GetAccesories().ToList().Where(a => a.Id == selectedAccessory.Id).FirstOrDefault();
                    bookVM.BookingAccessories.Add(selectedAccessory);
                }

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckBooking(BookingViewModel bookVM)
        {
            DiscountViewModel  discountVM = new DiscountViewModel();
            bookVM.Booking = dbBooking.GetBookings().Where(x => x.Id.Equals(bookVM.Booking.Id)).FirstOrDefault();
            var customer = bookVM.Customer;

            if (checkCustomer(bookVM))
            {
                dbCustomer.Add(bookVM.Customer);
                dbBooking.AddCustomerToOrder(bookVM.Customer.Id, bookVM.Booking.Id);
            }
            else
            {
                bookVM.Customer = customer;
                ViewBag.ErrorMessage = "Alle velden moeten ingevuld zijn, behalve tussenvoegsel";
                return View("CustomerBooking", bookVM);
            }

            decimal price = Price(bookVM);

            decimal discount = discountVM.TotalDiscount(bookVM);

            if(discount != 0)
            {
                price = price * ((100 - discount) / 100);
            }


            bookVM.Booking.Payment = price;
            bookVM.Booking.Discount = discount;
            dbBooking.addPaymentToOrder(bookVM.Booking.Payment, bookVM.Booking.Id);
            dbBooking.AddDiscountToOrder(bookVM.Booking.Discount, bookVM.Booking.Id);

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmBooking(BookingViewModel bookVM)
        {
            bookVM.Booking = dbBooking.GetBookings().Where(x => x.Id.Equals(bookVM.Booking.Id)).FirstOrDefault();

            DateTime date = DateTime.Now;
            dbBooking.addConfirmToOrder(date, bookVM.Booking.Id);

            return RedirectToAction("Index", "Home");
        }

        #region money
        public decimal Price(BookingViewModel bookVM)
        {
            decimal price = 0;

            foreach (var animal in bookVM.Booking.Animals)
            {
                price += animal.Price;
            }
            foreach (var acc in bookVM.Booking.Accessories)
            {
                price += acc.Price;
            }

            return price;
        }
        #endregion

        #region CustomerValidation

        private bool checkCustomer(BookingViewModel bookVM)
        {
            var customer = bookVM.Customer;

            if(customer.Name == null)
            {
                return false;
            }
           if(customer.LastName == null)
            {
                return false;
            }
           if(customer.Email == null)
            {
                return false;
            }
           if(customer.Adres == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
