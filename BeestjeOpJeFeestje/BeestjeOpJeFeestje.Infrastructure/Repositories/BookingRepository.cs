using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        BoefContext context;

        public BookingRepository(BoefContext context)
        {
            this.context = context;
        }

        public void AddAnimalsToOrder(int animalId, int bookId)
        {
            var animal = context.Animals.Find(animalId);
            var booking = context.Bookings.Find(bookId);
            booking.Animals.Add(animal);
            context.SaveChanges();
        }

        public void AddAccessoriesToOrder(int AccessoryId, int bookId)
        {
            var accessory = context.Accessories.Find(AccessoryId);
            var booking = context.Bookings.Find(bookId);
            booking.Accessories.Add(accessory);
            context.SaveChanges();
        }

        public void AddCustomerToOrder(int customerId, int bookId)
        {
            var customer = context.Customers.Find(customerId);
            var booking = context.Bookings.Find(bookId);
            customer.Bookings.Add(booking);
            context.SaveChanges();
        }

        public void addPaymentToOrder(decimal amount, int bookId)
        {
            var booking = context.Bookings.Find(bookId);
            booking.Payment = amount;
            context.SaveChanges();
        }

        public void AddDiscountToOrder(decimal amount, int bookId)
        {
            var booking = context.Bookings.Find(bookId);
            booking.Discount = amount;
            context.SaveChanges();
        }

        public void addConfirmToOrder(DateTime date, int bookId)
        {
            var booking = context.Bookings.Find(bookId);
            booking.IsConfirmed = date;
            context.SaveChanges();
        }

        public void Add(Booking b)
        {
            foreach (var animal in b.Animals)
            {
                context.Animals.Attach(animal);
            }
            context.Bookings.Add(b);
            context.SaveChanges();
        }

        public void Edit(Booking b)
        {
            context.Entry(b).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public Booking FindById(int Id)
        {
            context.Bookings.Include("Animals").ToList();
            context.Bookings.Include("Accessories").ToList();
            var result = (from r in context.Bookings where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<Booking> GetBookings()
        {
            context.Bookings.Include("Animals");
            context.Bookings.Include("Accessoires");
            context.Bookings.Include("Customers");
            return context.Bookings;
        }

        public void Remove(int Id)
        {
            Booking b = context.Bookings.Find(Id);
            context.Bookings.Remove(b);
            context.SaveChanges();
        }
    }
}
