using SoulFly.Models;

namespace SoulFly.Repositories
{
    public interface IRoutinePosesRepository
    {
         void AddPosesToRoutine(RoutinePoses routinePoses);
        void DeletePoseFromRoutine(int routineId, int posesId);
         List<Poses> GetAllRoutinesPoses(int id);

    }
}