using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class DiscountChecks
    {
        DiscountViewModel discountViewModel;
        BookingViewModel bookingsViewModel;

        [TestInitialize]
        public void TestMethodInitialize()
        {
            discountViewModel = new DiscountViewModel();
            bookingsViewModel = new BookingViewModel();
        }

        [TestMethod]
        public void AnimalTypeDiscount()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Sneeuw";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Sneeuw";
            Mock<Animal> animal3 = new Mock<Animal>();
            animal3.Object.Category = "Sneeuw";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);
            animals.Add(animal3.Object);

            var result = discountViewModel.CheckTypes(animals);
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void AnimalTypeDiscount2()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Sneeuw";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Sneeuw";
            Mock<Animal> animal3 = new Mock<Animal>();
            animal3.Object.Category = "Jungle";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);
            animals.Add(animal3.Object);

            var result = discountViewModel.CheckTypes(animals);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AnimalNameDiscount()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Zebra";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            var result = discountViewModel.CheckName(animals);
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void AnimalNameDiscount2()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Aap";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            var result = discountViewModel.CheckName(animals);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void AnimalDuckDiscount()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Eend";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            int total = 0;
            while (total == 0)
            {
                total = total + discountViewModel.CheckDuck(animals);
            }

            Assert.AreEqual(50, total);
        }

        [TestMethod]
        public void AnimalDayMondayDiscount()
        {
            Mock<Booking> booking = new Mock<Booking>();
            booking.Object.Date = new DateTime(2020, 1, 13);

            var result = discountViewModel.CheckDate(booking.Object.Date.DayOfWeek);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void AnimalDayTuesdayDiscount()
        {
            Mock<Booking> booking = new Mock<Booking>();
            booking.Object.Date = new DateTime(2020, 1, 14);

            var result = discountViewModel.CheckDate(booking.Object.Date.DayOfWeek);
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void AnimalDayDiscount()
        {
            Mock<Booking> booking = new Mock<Booking>();
            booking.Object.Date = new DateTime(2020, 1, 15);

            var result = discountViewModel.CheckDate(booking.Object.Date.DayOfWeek);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TotalDiscountCheck()
        {
            bookingsViewModel.Booking.Date = new DateTime(2020, 1, 14);

            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Jungle";
            animal.Object.Name = "abcdefghijklmnopqr";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Jungle";
            animal2.Object.Name = "Zebra";
            Mock<Animal> animal3 = new Mock<Animal>();
            animal3.Object.Category = "Jungle";
            animal3.Object.Name = "Olifant";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);
            animals.Add(animal3.Object);

            bookingsViewModel.Booking.Animals = animals;

            var result = discountViewModel.TotalDiscount(bookingsViewModel);

            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public void TotalDiscountCheck2()
        {
            bookingsViewModel.Booking.Date = new DateTime(2020, 1, 14);

            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Jungle";
            animal.Object.Name = "abcdefghij";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Jungle";
            animal2.Object.Name = "Zebra";
            Mock<Animal> animal3 = new Mock<Animal>();
            animal3.Object.Category = "Jungle";
            animal3.Object.Name = "Olifant";


            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);
            animals.Add(animal3.Object);

            bookingsViewModel.Booking.Animals = animals;

            var result = discountViewModel.TotalDiscount(bookingsViewModel);

            Assert.AreEqual(45, result);
        }
    }
}
