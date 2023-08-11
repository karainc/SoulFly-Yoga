using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SoulFly.Models;
using SoulFly.Repositories;
using SoulFly.Utils;
using Azure;


namespace SoulFly.Repositories
{
    public class RoutinePosesRepository : BaseRepository, IRoutinePosesRepository
    {
        public RoutinePosesRepository(IConfiguration config) : base(config) { }

        public List<Poses> GetAllRoutinesPoses(int id) // i want all of the Poses assigned to a routine. I need to list the poses. Find them by Id

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT pose.ID as PosesId, pose.Name as PosesName
                        FROM Routine routine
                        JOIN RoutinePoses routinePoses on routine.Id = routinePoses.RoutineId
                        JOIN Poses poses on poses.Id = routinePoses.PosesId
                        WHERE routine.ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    var poses = new List<Poses>();
                    while (reader.Read())
                    {
                        poses.Add(new Poses()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("PosesId")),
                            Name = reader.GetString(reader.GetOrdinal("PosesName"))
                        });
                    }
                    reader.Close();
                    return poses;
                }
            }
        }
        public void AddPosesToRoutine(RoutinePoses routinePoses)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"INSERT INTO RoutinePoses (RoutineId, PoseId)
                        OUTPUT INSERTED.Id
                        VALUES (@routineId, @poseId)";
                    // set paramter values for the poses and routine ID
                    cmd.Parameters.AddWithValue("@routineId", routinePoses.RoutineId);
                    cmd.Parameters.AddWithValue("@poseId", routinePoses.PoseId);

                    routinePoses.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeletePoseFromRoutine(int routineId, int posesId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"DELETE FROM RoutinePoses WHERE RoutineId = @routineId AND PosesId = @posesId";
                    cmd.Parameters.AddWithValue("@routineId", routineId);
                    cmd.Parameters.AddWithValue("@posesId", posesId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}