using SoulFly.Models;

namespace SoulFly.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void DeleteComment(int id);
        List<Comment> GetCommentsByRoutineId(int routineId);
        Comment GetCommentById(int id);
        void UpdateComment(Comment comment);
    }
}