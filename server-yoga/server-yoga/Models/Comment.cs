
using System.ComponentModel.DataAnnotations;

namespace server_yoga.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UsersId { get; set; }
        public int RoutineId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Users? Users { get; internal set; }
        public Routine? Routine { get; internal set; }
        public Poses? Poses { get; internal set; }
    }
}
