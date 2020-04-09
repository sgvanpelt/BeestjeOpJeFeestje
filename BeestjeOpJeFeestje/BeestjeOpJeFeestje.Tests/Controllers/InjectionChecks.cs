using System;
using System.Linq;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;
using BeestjeOpJeFeestje.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BeestjeOpJeFeestje.Tests.Controllers
{
    [TestClass]
    public class InjectionChecks
    {
        IAnimalRepository AnimalRepo;
        IAccesoryRepository AccessoryRepo;
        ICustomerRepository CustomerRepo;
        IBookingRepository BookingRepo;

        [TestInitialize]
        public void TestSetup()
        {
            BoefInitalizeDB db = new BoefInitalizeDB();
            System.Data.Entity.Database.SetInitializer(db);
            AnimalRepo = UnityConfig.MyResolve<IAnimalRepository>();
            AccessoryRepo = UnityConfig.MyResolve<IAccesoryRepository>();
            CustomerRepo = UnityConfig.MyResolve<ICustomerRepository>();
            BookingRepo = UnityConfig.MyResolve<IBookingRepository>();
        }
        [TestMethod]
        public void IsRepositoryInitalizeWithValidNumberOfDataAnimals()
        {
            var result = AnimalRepo.GetAnimals();
            var count = AnimalRepo.GetAnimals().Count();
            Assert.IsNotNull(result);
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);
        }

        [TestMethod]
        public void IsRepositoryInitalizeWithValidNumberOfDataAccossories()
        {
            var result = AccessoryRepo.GetAccesories();
            var count = AccessoryRepo.GetAccesories().Count();
            Assert.IsNotNull(result);
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);
        }

        [TestMethod]
        public void IsRepositoryAddsAnimal()
        {
            var count = AnimalRepo.GetAnimals().Count() + 1;
            Animal animal = new Animal();
            {               
                animal.Name = "Salt";
                animal.Category = "Jungle";
                animal.Price = 15;
                animal.Image = "~/Images/kuiken.png";
            }
            AnimalRepo.Add(animal);

            var result = AnimalRepo.GetAnimals();
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);

            AnimalRepo.Remove(animal.Id);
        }

        [TestMethod]
        public void IsRepositoryFindsAnimal()
        {
            var id = 1;
            var result = AnimalRepo.FindById(Convert.ToInt32(id));

            Assert.AreEqual("Aap" , result.Name);
        }

        [TestMethod]
        public void IsRepositoryGetsAnimals()
        {
            var count = AnimalRepo.GetAnimals().Count();
            Assert.IsNotNull(count);
        }

        [TestMethod]
        public void IsRepositoryAddsAccessory()
        {
            var count = AccessoryRepo.GetAccesories().Count() + 1;
            Accessory accessory = new Accessory();
            {
                accessory.Name = "Peper";
                accessory.Price = 15;
            }
            AccessoryRepo.Add(accessory);
 
            var result = AccessoryRepo.GetAccesories();
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);

            AccessoryRepo.Remove(accessory.Id);
        }

        [TestMethod]
        public void IsRepositoryFindsAccessory()
        {
            var id = 1;
            var result = AccessoryRepo.FindById(Convert.ToInt32(id));

            Assert.AreEqual("Banaan", result.Name);
        }

        [TestMethod]
        public void IsRepositoryGetsAccessory()
        {
            var count = AccessoryRepo.GetAccesories().Count();
            Assert.IsNotNull(count);
        }

        [TestMethod]
        public void IsRepositoryAddsCustomer()
        {
            var count = CustomerRepo.GetCustomers().Count() + 1;
            Customer customer = new Customer();
            {
                customer.Name = "Harry";
                customer.Insertion = "van";
                customer.LastName = "Boomsa";
                customer.Adres = "Jeroenlaan 3";
                customer.Email = "Jeroen@nick.com";
            }
            CustomerRepo.Add(customer);

            var result = CustomerRepo.GetCustomers();
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);

            CustomerRepo.Remove(customer.Id);
        }

        [TestMethod]
        public void IsRepositoryFindsCustomer()
        {
            var id = CustomerRepo.GetCustomers().ToList().FirstOrDefault().Id;
            var result = CustomerRepo.FindById(Convert.ToInt32(id));
            var result2 = CustomerRepo.GetCustomers().ToList().FirstOrDefault();

            Assert.AreEqual(result2.Name, result.Name);
        }

        [TestMethod]
        public void IsRepositoryAddsBooking()
        {
            var count = BookingRepo.GetBookings().Count() + 1;
            Booking booking = new Booking();
            {
                booking.Date = new DateTime(2020, 1, 31);
            }
            BookingRepo.Add(booking);

            var result = BookingRepo.GetBookings();
            var numberOfRecords = result.ToList().Count;
            Assert.AreEqual(count, numberOfRecords);

            BookingRepo.Remove(booking.Id);
        }

        [TestMethod]
        public void IsRepositoryFindsBooking()
        {
            var id = BookingRepo.GetBookings().ToList().FirstOrDefault().Id;
            var result = BookingRepo.FindById(Convert.ToInt32(id));
            var result2 = BookingRepo.GetBookings().ToList().FirstOrDefault();

            Assert.AreEqual(result2.Date, result.Date);
        }
    }
}
