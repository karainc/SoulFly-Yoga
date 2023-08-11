using SoulFly.Models;

namespace SoulFly.Repositories
{
    public interface IRoutineRepository
    {
        void AddRoutine(Routine routine);
        void DeleteRoutine(int id);
        List<Routine> GetAllRoutines();
        Routine GetRoutineById(int id);
        List<Routine> GetRoutinesByUserId(int userId);
        void UpdateRoutine(Routine routine);
    }
}