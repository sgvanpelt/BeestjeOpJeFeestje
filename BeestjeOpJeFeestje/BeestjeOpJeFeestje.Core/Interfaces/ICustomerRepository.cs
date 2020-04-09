using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer a);
        void Edit(Customer a);
        void Remove(int Id);
        IEnumerable<Customer> GetCustomers();
        Customer FindById(int Id);
    }
}
