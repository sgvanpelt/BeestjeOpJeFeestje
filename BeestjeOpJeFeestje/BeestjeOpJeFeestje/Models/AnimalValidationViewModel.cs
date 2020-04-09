using BeestjeOpJeFeestje.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Models
{
    public class AnimalValidationViewModel
    {
        public bool CheckFarm(BookingViewModel bookVM)
        {
            List<Animal> animals = new List<Animal>();
            List<string> names = new List<string>();

            foreach (var animal in bookVM.tempAnimals.Where(x => x.Category == "Boerderij"))
            {
                animals.Add(animal);
            }

            foreach (var animal in bookVM.tempAnimals)
            {
                names.Add(animal.Name);
            }

            if (animals.Count() != 0 && (names.Contains("Ijsbeer") || names.Contains("Leeuw")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckPinguïn(BookingViewModel bookVM)
        {
            List<string> names = new List<string>();

            var date = bookVM.Booking.Date;

            foreach (var animal in bookVM.tempAnimals)
            {
                names.Add(animal.Name);
            }

            if ((((date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday)) && names.Contains("Pinguïn")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckDesert(BookingViewModel bookVM)
        {
            List<string> categories = new List<string>();

            var date = bookVM.Booking.Date;

            foreach (var animal in bookVM.tempAnimals)
            {
                categories.Add(animal.Category);
            }

            if ((categories.Contains("Woestijn") && (date.Month > 9 || date.Month < 3)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckSnow(BookingViewModel bookVM)
        {
            List<string> categories = new List<string>();

            var date = bookVM.Booking.Date;

            foreach (var animal in bookVM.tempAnimals)
            {
                categories.Add(animal.Category);
            }

            if ((categories.Contains("Sneeuw") && (date.Month > 5 && date.Month < 9)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}