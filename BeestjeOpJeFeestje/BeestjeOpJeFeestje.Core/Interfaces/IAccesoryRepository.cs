using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core.Interfaces
{
    public interface IAccesoryRepository
    {
        void Add(Accessory a);
        void Edit(Accessory a);
        void Remove(int Id);
        IEnumerable<Accessory> GetAccesories();
        Accessory FindById(int Id);
    }
}
