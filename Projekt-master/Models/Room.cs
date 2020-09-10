using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Numer sali")]
        public string Nr { get; set; }
        public IList<Reservation> Reservations { get; set; }
    }
}
