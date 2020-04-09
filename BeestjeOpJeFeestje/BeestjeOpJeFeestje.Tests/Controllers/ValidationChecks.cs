using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeestjeOpJeFeestje;
using BeestjeOpJeFeestje.Controllers;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Core;
using Moq;
using BeestjeOpJeFeestje.Infrastructure.Repositories;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Infrastructure;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class ValidationChecks
    {

        AnimalValidationViewModel validation;
        BookingViewModel booking;

        [TestInitialize]
        public void Initialize()
        {
            validation = new AnimalValidationViewModel();
            booking = new BookingViewModel();
        }

        [TestMethod]
        public void CheckFarmAnimalLion()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Leeuw";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Boerderij";
            
            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);

            booking.tempAnimals = animals;

            var result = validation.CheckFarm(booking);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckFarmAnimalPolarBear()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Ijsbeer";
            Mock<Animal> animal2 = new Mock<Animal>();
            animal2.Object.Category = "Boerderij";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);
            animals.Add(animal2.Object);

            booking.tempAnimals = animals;

            var result = validation.CheckFarm(booking);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckWeekendAnimal()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Name = "Pinguïn";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            booking.tempAnimals = animals;
            booking.Booking.Date = new DateTime(2020, 1, 12);

            var result = validation.CheckPinguïn(booking);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckDesertAnimal()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Woestijn";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            booking.tempAnimals = animals;
            booking.Booking.Date = new DateTime(2020, 12, 31);

            var result = validation.CheckDesert(booking);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckSnowAnimal()
        {
            Mock<Animal> animal = new Mock<Animal>();
            animal.Object.Category = "Sneeuw";

            List<Animal> animals = new List<Animal>();
            animals.Add(animal.Object);

            booking.tempAnimals = animals;
            booking.Booking.Date = new DateTime(2020, 7, 31);

            var result = validation.CheckSnow(booking);

            Assert.IsFalse(result);
        }
    }

}
