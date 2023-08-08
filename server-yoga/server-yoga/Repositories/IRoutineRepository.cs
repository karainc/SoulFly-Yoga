using server_yoga.Models;

namespace server_yoga.Repositories
{
    public interface IRoutineRepository
    {
        void AddRoutine(Routine routine);
        void DeleteRoutine(int id);
        List<Routine> GetAllRoutines();
        Routine GetRoutineById(int routineId);
        List<Routine> GetRoutinesByUserId(int userId);
        void UpdateRoutine(Routine routine);
    }
}