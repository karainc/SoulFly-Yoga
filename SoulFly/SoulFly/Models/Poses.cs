using System.ComponentModel.DataAnnotations;

namespace SoulFly.Models
{
    public class Poses
    {
        [Required]
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
