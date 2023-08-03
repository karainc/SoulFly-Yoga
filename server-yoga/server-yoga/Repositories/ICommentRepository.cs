using server_yoga.Models;

namespace server_yoga.Repositories
{
    public interface ICommentRepository
    {
        void Add(Comment comment);
        void DeleteComment(int id);
        List<Comment> GetAllByRoutineId(int routineId);
        Comment GetCommentById(int id);
        void UpdateComment(Comment comment);
    }
}