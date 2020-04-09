using BeestjeOpJeFeestje.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    [ExcludeFromCodeCoverage]
    public class BookingViewModel
    {
        public Booking Booking { get; set; }

        public Customer Customer { get; set; }

        public Animal Animal { get; set; }

        public List<Accessory> Accessories { get; set; }

        public List<Animal> Animals { get; set; }

        public List<Animal> BookingAnimals { get; set; }

        public List<Animal> tempAnimals { get; set; }

        public List<Accessory> BookingAccessories { get; set; }

        public List<Booking> Bookings { get; set; }

        public int DiscountDuck { get; set; }

        public int DiscountTypes { get; set; }

        public int DiscountDay { get; set; }

        public int DiscountName { get; set; }

        public BookingViewModel()
        {
            Animal = new Animal();
            Booking = new Booking();
            Customer = new Customer();
            Accessories = new List<Accessory>();
            Animals = new List<Animal>();
            BookingAnimals = new List<Animal>();
            tempAnimals = new List<Animal>();
            BookingAccessories = new List<Accessory>();
            Bookings = new List<Booking>();
        }
       
    }
}