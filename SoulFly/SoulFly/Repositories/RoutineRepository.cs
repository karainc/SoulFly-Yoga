using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using SoulFly.Models;
using SoulFly.Utils;

namespace SoulFly.Repositories
{
    public class RoutineRepository : BaseRepository, IRoutineRepository
    {
        public RoutineRepository(IConfiguration configuration) : base(configuration) { }

        private Routine NewRoutineFromReader(SqlDataReader reader)
        {
            return new Routine()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Intention = reader.GetString(reader.GetOrdinal("Intention")),
                Reflection = reader.GetString(reader.GetOrdinal("Reflection")),
                CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                Cycles = reader.GetInt32(reader.GetOrdinal("Cycles")),
                PoseId = reader.GetInt32(reader.GetOrdinal("PoseId")),
                Poses = new Poses()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("PoseName")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Image = reader.GetString(reader.GetOrdinal("Image"))

                },
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                     Users = new Users()
                         {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                            Birthday = reader.GetString(reader.GetOrdinal("Birthday")),
                            Password = reader.GetString(reader.GetOrdinal("Password")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))

                         }
            };
        }
        public List<Routine> GetAllRoutines()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.Intention, r.Cycles, r.PoseId, r.CreationDate, r.Reflection, r.UserId, 
                            p.[Name] as PoseName, p.Description, p.Image,
                            u.DisplayName, u.Birthday, u.Email, u.Password                     
                        FROM Routine r
                            LEFT JOIN Poses p ON r.PoseId = p.Id
                            LEFT JOIN Users u ON r.UserId = u.Id
          
                        ORDER BY r.CreationDate desc
                    ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    var routines = new List<Routine>();

                    while (reader.Read())
                    {
                        routines.Add(NewRoutineFromReader(reader));
                    }
                    reader.Close();

                    return routines;
                }
            }
        }
        public List<Routine> GetRoutinesByUserId(int userId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.Intention, r.Cycles, r.Reflection, r.CreationDate, r.PoseId, r.UserId,
                            p.[Name] as PosesName, p.Description, p.Image,
                            u.DisplayName, u.Birthday, u.Email, u.Password
                        FROM Routine r
                            LEFT JOIN Poses p ON r.PoseId = p.Id
                            LEFT JOIN Users u ON r.UserId = u.Id
                        WHERE UserId = 1 AND CreationDate <= SYSDATETIME() AND r.UserId = @userId
                        ORDER BY r.CreationDate desc
                    ";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    var reader = cmd.ExecuteReader();

                    var routines = new List<Routine>();

                    while (reader.Read())
                    {
                        routines.Add(NewRoutineFromReader(reader));
                    }
                    reader.Close();

                    return routines;
                }
            }
        }
        public Routine GetRoutineById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.Intention, r.Cycles, r.Reflection, r.CreationDate, r.UserId, r.PoseId,
                            p.[Name] as PosesName, p.Description, p.Image,
                            u.DisplayName, u.Birthday, u.Email, u.Password
                        FROM Routine r
                            LEFT JOIN Poses p ON r.PoseId = p.Id
                            LEFT JOIN Users u ON r.UserId = u.Id
                        WHERE CreationDate <= SYSDATETIME()
                        AND r.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Routine routine = null;

                    if (reader.Read())
                    {
                        routine = NewRoutineFromReader(reader);
                    }

                    reader.Close();

                    return routine;
                }
            }
        }
        public void AddRoutine(Routine routine)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Routine (
                            Intention, Cycles, Reflection, CreationDate,
                            UserId, PoseId )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @Intention, @Cycles, @Reflection, @CreationDate,
                            @UserId, @PoseId )";
                    cmd.Parameters.AddWithValue("@Intention", routine.Intention);
                    cmd.Parameters.AddWithValue("@Cycles", routine.Cycles);
                    cmd.Parameters.AddWithValue("@Reflection", routine.Reflection);
                    cmd.Parameters.AddWithValue("@CreationDate", DbUtils.ValueOrDBNull(routine.CreationDate));
                    cmd.Parameters.AddWithValue("@UserId", routine.UserId);
                    cmd.Parameters.AddWithValue("@PosesId", routine.PoseId);

                    routine.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void UpdateRoutine(Routine routine)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Routine
                        SET
                        [Intention] = @intention,
                         [Cycles] = @cycles,
                         [Reflection] = @reflection,
                         [CreationDate] = @creationDate,
                         [PoseId] = @poseId,
                         [UserId] = @userId
                        WHERE Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", routine.Id);
                    cmd.Parameters.AddWithValue("@intention", routine.Intention);
                    cmd.Parameters.AddWithValue("@cycles", routine.Cycles);
                    cmd.Parameters.AddWithValue("@reflection", routine.Reflection);
                    cmd.Parameters.AddWithValue("@creationDate", routine.CreationDate);
                    cmd.Parameters.AddWithValue("@userId", routine.UserId);
                    cmd.Parameters.AddWithValue("@poseId", routine.PoseId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteRoutine(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"
                DELETE FROM Routine
                WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}