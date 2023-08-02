
using System.ComponentModel.DataAnnotations;

namespace server_yoga.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Text { get; set; }

        public int UsersId { get; set; }

        public int RoutineId { get; set; }
    }
}
