using BeestjeOpJeFeestje.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Infrastructure
{
    public class BoefContext : DbContext
    {
        public BoefContext() : base("name=BoefAppConnectionString")
        {
            System.Data.Entity.Database.SetInitializer(new BoefInitalizeDB());
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Accessory> Accessories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}
