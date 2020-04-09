using System;
using System.Linq;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class ControllerChecks
    {
        IAnimalRepository AnimalRepo;
        IAccesoryRepository AccessoryRepo;
        ICustomerRepository CustomerRepo;
        IBookingRepository BookingRepo;
        BookingsController bookingController;
        HomeController homeController;
        AnimalsController animalsController;
        AccessoriesController accessoriesController;
        BookingManageController bookingManageController;
        BookingViewModel bookingViewModel;

        [TestInitialize]
        public void TestSetup()
        {
            AnimalRepo = UnityConfig.MyResolve<IAnimalRepository>();
            AccessoryRepo = UnityConfig.MyResolve<IAccesoryRepository>();
            CustomerRepo = UnityConfig.MyResolve<ICustomerRepository>();
            BookingRepo = UnityConfig.MyResolve<IBookingRepository>();
            bookingController = new BookingsController(BookingRepo, AccessoryRepo, AnimalRepo, CustomerRepo);
            homeController = new HomeController(BookingRepo);
            animalsController = new AnimalsController(AnimalRepo, AccessoryRepo);
            accessoriesController = new AccessoriesController(AccessoryRepo, AnimalRepo);
            bookingManageController = new BookingManageController(BookingRepo, CustomerRepo);
            bookingViewModel = new BookingViewModel();
        }

        [TestMethod]
        public void HomeIndex()
        {
            var result = homeController.Index() as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void HomeBooking()
        {
            Booking booking = new Booking();
            booking.Date = new DateTime(2020, 1, 31);

            var result = homeController.SaveBooking(booking);

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void AnimalsIndex()
        {
            var result = animalsController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void AnimalsDetails()
        {
            var result = animalsController.Details(2) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AnimalsCreate()
        {
            var result = animalsController.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AnimalsAdd()
        {
            Animal animal = new Animal();
            animal.Name = "Jeroen";
            animal.Price = 100;
            animal.Category = "Boerderij";

            var result = animalsController.Create(animal) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());

            AnimalRepo.Remove(animal.Id);
        }

        [TestMethod]
        public void AnimalsEdits()
        {
            var result = animalsController.Edit(2) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AnimalsEdit()
        {
            Animal animal = new Animal();
            animal.Name = "Jeroen";
            animal.Price = 100;
            animal.Category = "Boerderij";
 
            AnimalRepo.Add(animal);

            animal.Name = "Chris";

            var result = animalsController.Edit(animal) as RedirectToRouteResult;

            Assert.AreEqual("Chris", AnimalRepo.FindById(animal.Id).Name);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());
            AnimalRepo.Remove(animal.Id);
        }

        [TestMethod]
        public void AnimalsRemove()
        {
            var result = animalsController.Delete(2) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AnimalsDelete()
        {
            Animal animal = new Animal();
            animal.Name = "Jeroen";
            animal.Price = 100;
            animal.Category = "Boerderij";
            AnimalRepo.Add(animal);

            var result = animalsController.DeleteConfirmed(animal.Id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());
        }

        [TestMethod]
        public void AnimalListBooking()
        {
            bookingViewModel.Booking.Date = new DateTime(2020, 1, 31);

            var result = bookingController.AnimalsBooking(bookingViewModel.Booking) as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void AccessoriesIndex()
        {
            var result = accessoriesController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessoriesDetails()
        {
            var result = accessoriesController.Details(2) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessoriesCreate()
        {
            var result = accessoriesController.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessoriesAdd()
        {
            Accessory accessory  = new Accessory();
            accessory.Name = "Jeroen";
            accessory.Price = 100;
            accessory.AnimalName = "Aap";

            var result = accessoriesController.Create(accessory) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());

            AccessoryRepo.Remove(accessory.Id);
        }

        [TestMethod]
        public void AccessoriesEdits()
        {
            var result = accessoriesController.Edit(2) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessoriesEdit()
        {
            Accessory accessory = new Accessory();
            accessory.Name = "Jeroen";
            accessory.Price = 100;

            AccessoryRepo.Add(accessory);

            accessory.Name = "Chris";

            var result = accessoriesController.Edit(accessory) as RedirectToRouteResult;

            Assert.AreEqual("Chris", AccessoryRepo.FindById(accessory.Id).Name);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());

            AccessoryRepo.Remove(accessory.Id);
        }

        [TestMethod]
        public void AccessoriesRemove()
        {
            var result = accessoriesController.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccessoriesDelete()
        {
            Accessory accessory = new Accessory();
            accessory.Name = "Jeroen";
            accessory.Price = 100;
            AccessoryRepo.Add(accessory);

            var result = accessoriesController.DeleteConfirmed(accessory.Id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());
        }

        [TestMethod]
        public void AccessoriesBooking()
        {
            bookingViewModel.Booking.Date = new DateTime(2020, 1, 31);

            var result = bookingController.CustomerBooking(bookingViewModel) as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void BookingIndex()
        {
            var result = bookingManageController.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BookingDetails()
        {
            var result = bookingManageController.Details(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BookingRemove()
        {
            var result = bookingManageController.Delete(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BookingDelete()
        {
            Booking booking = new Booking();
            booking.Date = new DateTime(2020, 1, 31);
            BookingRepo.Add(booking);

            var result = bookingManageController.DeleteConfirmed(booking.Id) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.IsNotNull(result.ToString());
        }
    }
}
