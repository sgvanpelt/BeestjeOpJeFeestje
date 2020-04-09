using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        BoefContext context = new BoefContext();

        public void Add(Customer c)
        {
            context.Customers.Add(c);
            context.SaveChanges();
        }

        public void Edit(Customer c)
        {
            context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public Customer FindById(int Id)
        {
            var result = (from r in context.Customers where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            context.Animals.Include("Bookings").ToList();
            return context.Customers;
        }

        public void Remove(int Id)
        {
            Customer c = context.Customers.Find(Id);
            context.Customers.Remove(c);
            context.SaveChanges();
        }
    }
}
