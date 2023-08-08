using server_yoga.Models;

namespace server_yoga.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void DeleteComment(int id);
        List<Comment> GetAllByRoutineId(int routineId);
        Comment GetCommentById(int id);
        void UpdateComment(Comment comment);
    }
}