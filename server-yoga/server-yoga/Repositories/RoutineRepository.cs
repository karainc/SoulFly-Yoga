﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Hosting;
using server_yoga.Models;
using server_yoga.Utils;

namespace server_yoga.Repositories
{
    public class RoutineRepository : BaseRepository
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
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                Cycles = reader.GetInt32(reader.GetOrdinal("Cycles")),
                PoseId = reader.GetInt32(reader.GetOrdinal("PoseId")),
                Poses = new Poses()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("PosesId")),
                    Name = reader.GetString(reader.GetOrdinal("PosesName")),
                    Image = DbUtils.GetString(reader, "Image"),
                },
                UsersId = reader.GetInt32(reader.GetOrdinal("UsersId")),
                Users = new Users()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UsersId")),
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
                        SELECT r.Id, r.Title, r.Content, r.ImageLocation AS ImageUrl, r.CreationDate, r.PublishDateTime, r.IsApproved, p.CategoryId, p.UsersId,
                            c.[Name] as CategoryName,
                            u.DisplayName, u.Birthday, u.DisplayName, u.Email, u.CreationDate, u.ImageLocation as UserImageUrl, u.UserTypeId,
                            ut.[Name] as UserTypeName
                        FROM Routine p
                            LEFT JOIN Category c ON p.CategoryId = c.Id
                            LEFT JOIN Users u ON p.UsersId = u.Id
                            LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                        WHERE IsApproved = 1 AND PublishDateTime <= SYSDATETIME()
                        ORDER BY PublishDateTime desc
                    ";
                    var reader = cmd.ExecuteReader();

                    var routine = new List<Routine>();

                    while (reader.Read())
                    {
                        routine.Add(NewRoutineFromReader(reader));
                    }
                    reader.Close();

                    return routine;
                }
            }
        }
        public List<Routine> GetRoutinesByUserId(int usersId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.Id, p.Title, p.Content, p.ImageLocation AS ImageUrl, p.CreationDate, p.PublishDateTime, p.IsApproved, p.CategoryId, p.UsersId,
                            c.[Name] as CategoryName,
                            u.DisplayName, u.Birthday, u.DisplayName, u.Email, u.CreationDate, u.ImageLocation as UserImageUrl, u.UserTypeId,
                            ut.[Name] as UserTypeName
                        FROM Routine p
                            LEFT JOIN Category c ON p.CategoryId = c.Id
                            LEFT JOIN Users u ON p.UsersId = u.Id
                            LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                        WHERE IsApproved = 1 AND PublishDateTime <= SYSDATETIME() AND p.UsersId = @usersId
                        ORDER BY p.CreationDate desc
                    ";
                    cmd.Parameters.AddWithValue("@usersId", usersId);
                    var reader = cmd.ExecuteReader();

                    var routine = new List<Routine>();

                    while (reader.Read())
                    {
                        routine.Add(NewRoutineFromReader(reader));
                    }
                    reader.Close();

                    return routine;
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
                        SELECT p.Id, p.Title, p.Content, p.ImageLocation AS ImageUrl, p.CreationDate, p.PublishDateTime, p.IsApproved, p.CategoryId, p.UsersId,
                            c.[Name] as CategoryName,
                            u.DisplayName, u.Birthday, u.DisplayName, u.Email, u.CreationDate, u.ImageLocation as UserImageUrl, u.UserTypeId,
                            ut.[Name] as UserTypeName
                        FROM Routine p
                            LEFT JOIN Category c ON p.CategoryId = c.Id
                            LEFT JOIN Users u ON p.UsersId = u.Id
                            LEFT JOIN UserType ut ON u.UserTypeId = ut.Id
                        WHERE IsApproved = 1 AND PublishDateTime <= SYSDATETIME()
                        AND p.Id = @id";

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
                            Title, Content, ImageLocation, CreationDate, PublishDateTime,
                            IsApproved, CategoryId, UsersId )
                        OUTPUT INSERTED.ID
                        VALUES (
                            @Title, @Content, @ImageLocation, @CreationDate, @PublishDateTime,
                            @IsApproved, @CategoryId, @UsersId )";
                    cmd.Parameters.AddWithValue("@Title", routine.Title);
                    cmd.Parameters.AddWithValue("@Content", routine.Content);
                    cmd.Parameters.AddWithValue("@ImageLocation", DbUtils.ValueOrDBNull(routine.ImageLocation));
                    cmd.Parameters.AddWithValue("@CreationDate", routine.CreationDate);
                    cmd.Parameters.AddWithValue("@PublishDateTime", DbUtils.ValueOrDBNull(routine.PublishDateTime));
                    cmd.Parameters.AddWithValue("@IsApproved", routine.IsApproved);
                    cmd.Parameters.AddWithValue("@CategoryId", routine.CategoryId);
                    cmd.Parameters.AddWithValue("@UsersId", routine.UsersId);

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
                        [Title] = @title,
                         [Content] = @content,
                         [ImageLocation] = @imageLocation,
                         [CreationDate] = @creationDate,
                         [PublishDateTime] = @publishDateTime,
                         [CategoryId] = @categoryId,
                         [UsersId] = @usersId
                        WHERE Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", routine.Id);
                    cmd.Parameters.AddWithValue("@title", routine.Title);
                    cmd.Parameters.AddWithValue("@content", routine.Content);
                    cmd.Parameters.AddWithValue("@imageLocation", routine.ImageLocation);
                    cmd.Parameters.AddWithValue("@creationDate", routine.CreationDate);
                    cmd.Parameters.AddWithValue("@publishDateTime", routine.PublishDateTime);
                    cmd.Parameters.AddWithValue("@isApproved", routine.IsApproved);
                    cmd.Parameters.AddWithValue("@categoryId", routine.CategoryId);
                    cmd.Parameters.AddWithValue("@usersId", routine.UsersId);

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
            }
        }
    }
}
