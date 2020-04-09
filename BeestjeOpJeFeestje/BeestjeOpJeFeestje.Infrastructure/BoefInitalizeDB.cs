using BeestjeOpJeFeestje.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class BoefInitalizeDB : DropCreateDatabaseIfModelChanges<BoefContext>
    {
        protected override void Seed(BoefContext context)
        {
            context.Accessories.Add(new Accessory() { Id = 1, Name = "Banaan", Price = 10, Image = "~/Images/accossoire.jpg" });
            context.Accessories.Add(new Accessory() { Id = 2, Name = "Zadel", Price = 50, Image = "~/Images/accossoire.jpg" });
            context.Accessories.Add(new Accessory() { Id = 3, Name = "Krukje", Price = 40, Image = "~/Images/accossoire.jpg" });
            context.Accessories.Add(new Accessory() { Id = 4, Name = "Zweep", Price = 30, Image = "~/Images/accossoire.jpg" });
            context.Accessories.Add(new Accessory() { Id = 5, Name = "Bal", Price = 25, Image = "~/Images/accossoire.jpg" });
            context.Accessories.Add(new Accessory() { Id = 6, Name = "Dansschoenen", Price = 60, Image = "~/Images/accossoire.jpg" });

            var Monkey = new Animal() { Id = 1, Name = "Aap", Category = "Jungle", Price = 70, Image="~/Images/aap.png" };
            Monkey.Accessories.Add(context.Accessories.Find(1));
            context.Animals.Add(Monkey);

            var Elephant = new Animal() { Id = 2, Name = "Olifant", Category = "Jungle", Price = 100, Image = "~/Images/olifant.png" };
            context.Animals.Add(Elephant);

            var Zebra = new Animal() { Id = 3, Name = "Zebra", Category = "Jungle", Price = 70, Image = "~/Images/zebra.png" };
            Zebra.Accessories.Add(context.Accessories.Find(2));
            context.Animals.Add(Zebra);

            var Lion = new Animal() { Id = 4, Name = "Leeuw", Category = "Jungle", Price = 100, Image = "~/Images/leeuw.png" };
            Lion.Accessories.Add(context.Accessories.Find(3));
            Lion.Accessories.Add(context.Accessories.Find(4));
            context.Animals.Add(Lion);

            var Dog = new Animal() { Id = 5, Name = "Hond", Category = "Boerderij", Price = 50, Image = "~/Images/doggo.png" };
            Dog.Accessories.Add(context.Accessories.Find(5));
            context.Animals.Add(Dog);

            var Donkey = new Animal() { Id = 6, Name = "Ezel", Category = "Boerderij", Price = 50, Image = "~/Images/donkey.png" };
            context.Animals.Add(Donkey);

            var Cow = new Animal() { Id = 7, Name = "Koe", Category = "Boerderij", Price = 50, Image = "~/Images/koe.png" };
            context.Animals.Add(Cow);

            var Duck = new Animal() { Id = 8, Name = "Eend", Category = "Boerderij", Price = 30, Image = "~/Images/duck.png" };
            context.Animals.Add(Duck);

            var Chicken = new Animal() { Id = 9, Name = "Kuiken", Category = "Boerderij", Price = 30, Image = "~/Images/kip.png" };
            context.Animals.Add(Chicken);

            var Penguin = new Animal() { Id = 10, Name = "Pinguïn", Category = "Sneeuw", Price = 60, Image = "~/Images/pingwing.png" };
            Penguin.Accessories.Add(context.Accessories.Find(6));
            context.Animals.Add(Penguin);

            var Polarbear = new Animal() { Id = 11, Name = "Ijsbeer", Category = "Sneeuw", Price = 100, Image = "~/Images/ijsbeer.png" };
            context.Animals.Add(Polarbear);

            var Seal = new Animal() { Id = 12, Name = "Zeehond", Category = "Sneeuw", Price = 60, Image = "~/Images/zeehond.png" };
            context.Animals.Add(Seal);

            var Camel = new Animal() { Id = 13, Name = "Kameel", Category = "Woestijn", Price = 70, Image = "~/Images/kuiken.png" };
            context.Animals.Add(Camel);

            var Snake = new Animal() { Id = 14, Name = "Slang", Category = "Woestijn", Price = 50, Image = "~/Images/kuiken.png" };
            context.Animals.Add(Snake);
            context.SaveChanges(); 
            base.Seed(context);
        }
    }
}
