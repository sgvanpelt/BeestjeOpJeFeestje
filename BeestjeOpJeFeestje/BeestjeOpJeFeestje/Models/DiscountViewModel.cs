using BeestjeOpJeFeestje.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class DiscountViewModel
    {

        public int CheckTypes(List<Animal> animals)
        {
            int jungle = 0;
            int farm = 0;
            int snow = 0;
            int desert = 0;

            foreach (Animal animal in animals)
            {
                switch (animal.Category.ToLower())
                {
                    case "jungle":
                        jungle++;
                        break;
                    case "boerderij":
                        farm++;
                        break;
                    case "sneeuw":
                        snow++;
                        break;
                    case "woestijn":
                        desert++;
                        break;
                }
            }

            if (jungle == 3 || farm == 3 || snow == 3 || desert == 3)
            {
                return 10;
            }

            return 0;
        }

        public int CheckDuck(List<Animal> animals)
        {
            List<string> names = new List<string>();

            foreach(Animal animal in animals)
            {
                names.Add(animal.Name);
            }

            if (names.Contains("Eend"))
            {
                var random = new Random();
                var number = random.Next(1, 7);

                if(number == 5)
                {
                    return 50;
                }
            }
            return 0;
        }

        public int CheckDate(DayOfWeek day)
        {
            if(day == DayOfWeek.Monday || day == DayOfWeek.Tuesday)
            {
                return 15;
            }
            return 0;
        }

        public int CheckName(List<Animal> animals)
        {
            var discount = 0;
            bool hasChar = true;
            string name = null;
            foreach (Animal animal in animals)
            {
                name += animal.Name;
                
            }
            for (char c = 'a'; c <= 'z'; c++)
            {
                if (hasChar)
                {
                    if (name.Contains(c))
                    {
                        discount += 2;

                    }
                    else
                    {
                        hasChar = false;
                    }
                }
            }
            return discount;
        }

        public decimal TotalDiscount(BookingViewModel bookVM)
        {
            decimal TotalDiscount = 0;

            TotalDiscount += CheckDuck(bookVM.Booking.Animals.ToList());
            bookVM.DiscountDuck = CheckDuck(bookVM.Booking.Animals.ToList());
            TotalDiscount += CheckTypes(bookVM.Booking.Animals.ToList());
            bookVM.DiscountTypes = CheckTypes(bookVM.Booking.Animals.ToList());

            if (TotalDiscount < 60)
            {
                TotalDiscount += CheckDate(bookVM.Booking.Date.DayOfWeek);
                bookVM.DiscountDay = CheckDate(bookVM.Booking.Date.DayOfWeek);
                if (TotalDiscount < 60)
                {
                    TotalDiscount += CheckName(bookVM.Booking.Animals.ToList());
                    bookVM.DiscountName = CheckName(bookVM.Booking.Animals.ToList());
                }
                else
                {
                    TotalDiscount = 60;
                }
            }
            else
            {
                TotalDiscount = 60;
            }

            if(TotalDiscount > 60)
            {
                TotalDiscount = 60;
            }

            return TotalDiscount;
        }

    }
}