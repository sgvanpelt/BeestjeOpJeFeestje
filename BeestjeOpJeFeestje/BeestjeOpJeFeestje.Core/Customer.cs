using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Core
{
    public class Customer
    {
        public Customer()
        {
            this.Bookings = new List<Booking>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Display(Name = "Tussenvoegsel")]
        public string Insertion { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Telefoonnummer")]
        public int? Phonenumber { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
