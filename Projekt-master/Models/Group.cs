using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Grupa")]
        public string Name { get; set; }

        public IList<Reservation> Reservations { get; set; }
    }
}
