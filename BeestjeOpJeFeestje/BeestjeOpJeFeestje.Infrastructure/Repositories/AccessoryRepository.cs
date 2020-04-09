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
    public class AccessoryRepository : IAccesoryRepository
    {
        BoefContext context = new BoefContext();
        public void Add(Accessory a)
        {
            context.Accessories.Add(a);
            context.SaveChanges();
        }

        public void Edit(Accessory a)
        {
            context.Entry(a).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public Accessory FindById(int Id)
        {
            context.Accessories.Include("Bookings").ToList();
            var result = (from r in context.Accessories where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<Accessory> GetAccesories()
        {
            return context.Accessories;
        }

        public void Remove(int Id)
        {
            Accessory a = context.Accessories.Find(Id);
            context.Accessories.Remove(a);
            context.SaveChanges();
        }

    }
}
