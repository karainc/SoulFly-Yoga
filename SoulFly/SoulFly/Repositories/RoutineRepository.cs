using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using SoulFly.Models;
using SoulFly.Utils;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SoulFly.Repositories
{
    public class RoutineRepository : BaseRepository, IRoutineRepository
    {
        public RoutineRepository(IConfiguration configuration) : base(configuration) { }

        //private Routine NewRoutineFromReader(SqlDataReader reader)
        //{
        //    return new Routine()
        //    {
        //        Id = DbUtils.GetInt(reader,"Id")),
        //        Intention = reader.GetString(reader.GetOrdinal("Intention")),
        //        Reflection = reader.GetString(reader.GetOrdinal("Reflection")),
        //        CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
        //        Cycles = reader.GetInt32(reader.GetOrdinal("Cycles")),
        //        PoseId = reader.GetInt32(reader.GetOrdinal("PoseId")),
        //        Poses = new Poses()
        //        {
        //            Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //            Name = reader.GetString(reader.GetOrdinal("PosesName")),
        //            Description = reader.GetString(reader.GetOrdinal("Description")),
        //            Image = reader.GetString(reader.GetOrdinal("Image"))

        //        },
        //        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
        //             Users = new Users()
        //                 {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        //                    Birthday = reader.GetString(reader.GetOrdinal("Birthday")),
        //                    Password = reader.GetString(reader.GetOrdinal("Password")),
        //                    Email = reader.GetString(reader.GetOrdinal("Email"))

        //                 }
        //    };
        //}
        public List<Routine> GetAllRoutines()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT r.Id, r.Intention, r.Cycles, r.PoseId, r.CreationDate, r.Reflection, r.UserId, 
                            p.[Name] as PosesName, p.Description, p.Image,
                            u.DisplayName, u.Birthday, u.Email, u.Password                     
                        FROM Routine r
                            LEFT JOIN Poses p ON r.PoseId = p.Id
                            LEFT JOIN Users u ON r.UserId = u.Id
          
                        ORDER BY r.CreationDate desc
                    ";
                    var reader = cmd.ExecuteReader();

                    var routines = new List<Routine>();

                    while (reader.Read())
                    {
                        routines.Add(new Routine()
                        {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Intention = DbUtils.GetString(reader, "Intention"),
                        Cycles = DbUtils.GetInt(reader, "Cycles"),
                        Reflection = DbUtils.GetString(reader, "Reflection"),
                        PoseId = DbUtils.GetInt(reader, "PoseId"),
                        CreationDate = DbUtils.GetDateTime(reader, "CreationDate"),
                        UserId = DbUtils.GetInt(reader, "UserId"),

                        Poses = new Poses
                        {
                            Name = DbUtils.GetString(reader, "PosesName"),
                            Description = DbUtils.GetString(reader, "Description"),
                            Image = DbUtils.GetString(reader, "Image")
                        },

                        Users = new Users
                        {
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Birthday = DbUtils.GetString(reader, "Birthday"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Password = DbUtils.GetString(reader, "Password")
                        },

                    });
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

                    DbUtils.AddParameter(cmd, "@userId", userId);
                    var reader = cmd.ExecuteReader();
                    var routines = new List<Routine>();

                    Routine routine = null;
                    while (reader.Read())
                    {
                        routine = new Routine();

                        routine.Id = DbUtils.GetInt(reader, "Id");
                        routine.Intention = DbUtils.GetString(reader, "Intention");
                        routine.Cycles = DbUtils.GetInt(reader, "Cycles");
                        routine.CreationDate = DbUtils.GetDateTime(reader, "CreationDate");
                        routine.Reflection = DbUtils.GetString(reader, "Reflection");
                        routine.PoseId = DbUtils.GetInt(reader, "PoseId");
                        routine.UserId = DbUtils.GetInt(reader, "UserId");

                        routine.Poses = new Poses
                        {
                            Name = DbUtils.GetString(reader, "PosesName"),
                            Description = DbUtils.GetString(reader, "Description"),
                            Image = DbUtils.GetString(reader, "Image")
                        };

                        routine.Users = new Users
                        {
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Birthday = DbUtils.GetString(reader,"Birthday"),
                            Email = DbUtils.GetString(reader,"Email"),
                            Password = DbUtils.GetString(reader, "Password")
                        };

                        routines.Add( routine );
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
                            u.DisplayName, u.Email
                        FROM Routine r
                            LEFT JOIN Poses p ON r.PoseId = p.Id
                            LEFT JOIN Users u ON r.UserId = u.Id
                        WHERE CreationDate <= SYSDATETIME()
                        AND r.Id = @id";

                    DbUtils.AddParameter(cmd,"@id", id);
                    var reader = cmd.ExecuteReader();

                    Routine routine = null;

                    if (reader.Read())
                    {
                        routine = new Routine();

                        routine.Id = DbUtils.GetInt(reader, "Id");
                        routine.Intention = DbUtils.GetString(reader, "Intention");
                        routine.Cycles = DbUtils.GetInt(reader, "Cycles");
                        routine.CreationDate = DbUtils.GetDateTime(reader, "CreationDate");
                        routine.Reflection = DbUtils.GetString(reader, "Reflection");
                        routine.PoseId = DbUtils.GetInt(reader, "PoseId");
                        routine.UserId = DbUtils.GetInt(reader, "UserId");

                        routine.Poses = new Poses
                        {
                            Name = DbUtils.GetString(reader, "PosesName"),
                            Description = DbUtils.GetString(reader, "Description"),
                            Image = DbUtils.GetString(reader, "Image")
                        };

                        routine.Users = new Users
                        {
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Email = DbUtils.GetString(reader, "Email"),
                        };
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

                    DbUtils.AddParameter(cmd, "@Intention", routine.Intention);
                    DbUtils.AddParameter(cmd,"@Cycles", routine.Cycles);
                    DbUtils.AddParameter(cmd,"@Reflection", routine.Reflection);
                    DbUtils.AddParameter(cmd,"@CreationDate", routine.CreationDate);
                    DbUtils.AddParameter(cmd,"@UserId", routine.UserId);
                    DbUtils.AddParameter(cmd,"@PoseId", routine.PoseId);

                    routine.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        //Edit
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
                         [PoseId] = @poseId,
                         [UserId] = @userId
                        WHERE Id = @id
                        ";
                    DbUtils.AddParameter(cmd, "@Id", routine.Id);
                    DbUtils.AddParameter(cmd,"@Intention", routine.Intention);
                    DbUtils.AddParameter(cmd,"@Cycles", routine.Cycles);
                    DbUtils.AddParameter(cmd,"@Reflection", routine.Reflection);
                    DbUtils.AddParameter(cmd,"@UserId", routine.UserId);
                    DbUtils.AddParameter(cmd,"@PoseId", routine.PoseId);

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

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}