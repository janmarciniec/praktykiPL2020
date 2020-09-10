using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Sala")]
        public int RoomId { get; set; }
        [DisplayName("Sala")]
        public Room Room { get; set; }

        [DisplayName("Godzina")]
        public int HourId { get; set; }
        [DisplayName("Godzina")]
        public Hour Hour { get; set; }

        [DisplayName("Nauczyciel")]
        public int TeacherId { get; set; }
        [DisplayName("Nauczyciel")]
        public Teacher Teacher { get; set; }

        [DisplayName("Grupa")]
        public int GroupId { get; set; }
        [DisplayName("Grupa")]
        public Group Group { get; set; }

        [DisplayName("Przedmiot")]
        public int SubjectId { get; set; }
        [DisplayName("Przedmiot")]
        public Subject Subject { get; set; }

        [DisplayName("Dzień")]
        public int DayId { get; set; }
        [DisplayName("Dzień")]
        public Day Day { get; set; }



    }
}
