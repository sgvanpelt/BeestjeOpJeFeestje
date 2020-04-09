using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Core
{
    public class Accessory
    {
        public Accessory()
        {
            this.Bookings = new List<Booking>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        [Display(Name = "Prijs")]
        public int Price { get; set; }

        [Display(Name = "Plaatje")]
        public string Image { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        [Display(Name = "Naam van dier")]
        public string AnimalName { get; set; }

        [Display(Name = "Bookings")]
        public ICollection<Booking> Bookings { get; set; }
    }
}