using server_yoga.Models;

namespace server_yogaRepositories
{
    public interface IRoutinePosesRepository
    {
        void AddRoutine(Routine routine);
        void DeleteRoutine(int id);
        List<Routine> GetAllRoutines();
        Routine GetRoutineById(int id);
        List<Routine> GetRoutinesByUserId(int userId);
        void UpdateRoutine(Routine routine);
    }
}