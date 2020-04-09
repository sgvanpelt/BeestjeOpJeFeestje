using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BeestjeOpJeFeestje.Core
{
    public class Animal
    {
        public Animal()
        {
            this.Accessories = new List<Accessory>();
            this.Bookings = new List<Booking>();
            this.PossibleAccessories = new List<Accessory>();
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

        [Required]
        [Display(Name = "Categorie")]
        public string Category { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        [NotMapped]
        public ICollection<Accessory> PossibleAccessories { get; set; }

        [Display(Name = "Plaatje")]
        public string Image { get; set; }

        [Display(Name = "Accessoires")]
        public ICollection<Accessory> Accessories { get; set; }

        [Display(Name = "Bookings")]
        public ICollection<Booking> Bookings { get; set; }
    }
}