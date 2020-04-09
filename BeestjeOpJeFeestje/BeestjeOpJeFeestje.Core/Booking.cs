using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core
{
    public class Booking
    {
        public Booking()
        {
            this.Accessories = new List<Accessory>();
            this.Animals = new List<Animal>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Bookingsdatum")]
        public DateTime Date { get; set; }

        [Display(Name = "Betaling")]
        public decimal Payment { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Korting")]
        public decimal Discount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Bevestigingsdatum")]
        public DateTime? IsConfirmed { get; set; }

        [NotMapped]
        public Customer customer { get; set; }

        [Display(Name = "Accessoires")]
        public ICollection<Accessory> Accessories { get; set; }

        [Display(Name = "Beestjes")]
        public ICollection<Animal> Animals { get; set; }

    }
}
