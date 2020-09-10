using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praktyki.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Login")]
        public string Login { get; set; }
        [Required]
        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}
