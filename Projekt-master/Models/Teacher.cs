using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class Teacher
    {

        [Key]
        public int Id { get; set; }
        [DisplayName("Stopień/tytuł naukowy")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Imię")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Nazwisko")]
        public string SecondName { get; set; }
        
        public IList<Reservation> Reservations { get; set; }


        public string Fullname
        {
            get
            {
                return string.Format("{0} {1} {2}", Title, FirstName, SecondName);
            }
        }
    }
}
