using System.ComponentModel.DataAnnotations;

namespace SoulFly.Models
{
    public class Routine
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string? Intention { get; set; }
        public int PoseId { get; set; }
        public int Cycles { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Reflection { get; set; }
        public Poses? Poses { get; internal set; }
        public int UsersId { get; internal set; }
        public Users? Users { get; internal set; }
    }
}
