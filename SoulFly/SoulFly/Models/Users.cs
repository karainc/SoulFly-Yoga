using System.ComponentModel.DataAnnotations;

namespace SoulFly.Models
{
     public class Users
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Birthday { get; set; }

    }
}
