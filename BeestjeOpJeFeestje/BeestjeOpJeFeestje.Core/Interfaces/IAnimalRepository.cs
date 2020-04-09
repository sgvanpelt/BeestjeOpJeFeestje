using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core.Interfaces
{
    public interface IAnimalRepository
    {
        void Add(Animal a);
        void Edit(Animal a);
        void Remove(int Id);
        IEnumerable<Animal> GetAnimals(); 
        Animal FindById(int Id);
        void AddAccessoireToAnimal(int animalId, int AccessoryId);
    }
}
