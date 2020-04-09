using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core.Interfaces
{
    public interface IBookingRepository
    {
        void Add(Booking a);
        void Edit(Booking a);
        void Remove(int Id);
        IEnumerable<Booking> GetBookings();
        Booking FindById(int Id);
        void AddAnimalsToOrder(int animalId, int bookId);
        void AddAccessoriesToOrder(int AccessoryId, int bookId);
        void AddCustomerToOrder(int customerId, int bookId);
        void addPaymentToOrder(decimal amount, int bookId);
        void addConfirmToOrder(DateTime date, int bookId);
        void AddDiscountToOrder(decimal amount, int bookId);
    }
}
