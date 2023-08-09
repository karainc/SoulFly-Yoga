using server_yoga.Models;

namespace server_yogaRepositories
{
    public interface IRoutinePosesRepository
    {
        public List<Poses> GetAllRoutinesPoses(int id);
        void AddPosesToRoutine(RoutinePoses routinePoses);
        void DeletePoseFromRoutine(int routineId, int posesId);
        
    }
}