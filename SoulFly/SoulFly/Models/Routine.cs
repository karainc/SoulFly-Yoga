using System.ComponentModel.DataAnnotations;

namespace SoulFly.Models
{
    public class Routine
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
       
        public string? Intention { get; set; }
        public int? PoseId { get; set; }
        public int? Cycles { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? Reflection { get; set; }
        public Poses? Poses { get; set; }
        public Users? Users { get; set; }


       
    }
}
