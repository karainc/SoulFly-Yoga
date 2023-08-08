using System.ComponentModel.DataAnnotations;

namespace server_yoga.Models
{
    public class RoutinePoses
    {
        [Required]
        public int Id { get; set; }

        public int RoutineId { get; set; }

        public int PoseId { get; set; }

        public int? CommentId { get; set; }

    }
}
