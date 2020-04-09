using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;

namespace BeestjeOpJeFeestje.Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        BoefContext context = new BoefContext();

        public void AddAccessoireToAnimal(int animalId, int AccessoryId)
        {
            var animal = context.Animals.Find(animalId);
            var accessory = context.Accessories.Find(AccessoryId);
            animal.Accessories.Add(accessory);
            context.SaveChanges();
        }

        public void Add(Animal a)
        {
            context.Animals.Add(a);
            context.SaveChanges();
        }

        public void Edit(Animal a)
        {
            context.Entry(a).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public Animal FindById(int Id)
        {
            context.Animals.Include("Bookings").ToList(); 
            context.Animals.Include("Accessories").ToList();
            var result = (from r in context.Animals where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<Animal> GetAnimals()
        {
            context.Animals.Include("Accessories").ToList();
            context.Animals.Include("Bookings").ToList();
            return context.Animals;
        }

        public void Remove(int Id)
        {
            Animal a = context.Animals.Find(Id); 
            context.Animals.Remove(a); 
            context.SaveChanges();
        }
    }
}
