using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Hosting;
using SoulFly.Models;
using SoulFly.Repositories;
using SoulFly.Utils;
using System.Data;

namespace SoulFly.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetCommentsByRoutineId(int routineId)
        {
            //List<Comment> comments = new List<Comment>();

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.Id, 
                    c.RoutineId, c.Text, c.CreateDateTime,
                    r.Intention AS RoutineIntention, r.Cycles AS RoutineCycles,
                    p.Name AS PoseName,
                    u.Id as UsersId, u.DisplayName AS UsersDisplayName
                    FROM Comment c
                    JOIN Routine r ON c.RoutineId = r.Id
                    LEFT JOIN Users u ON c.UserId = u.Id
                    LEFT JOIN Poses p ON r.PoseId = p.Id
                    WHERE c.RoutineId = @RoutineId
                    ORDER BY c.CreateDateTime DESC";

                    cmd.Parameters.AddWithValue("@RoutineId", routineId);

                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            RoutineId = reader.GetInt32(reader.GetOrdinal("routineId")),
                            UsersId = reader.GetInt32(reader.GetOrdinal("usersId")),
                            Text = reader.GetString(reader.GetOrdinal("text")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime)")),

                            Routine = new Routine()
                            {
                                Intention = reader.GetString(reader.GetOrdinal("intention")),
                                Cycles = reader.GetInt32(reader.GetOrdinal("cycles")),
                            },

                            Users = new Users()
                            {
                                //Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                DisplayName = reader.GetString(reader.GetOrdinal("displayName"))
                            },

                            Poses = new Poses
                            {
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            }

                        });
                    }

                    reader.Close();
                    return comments;
                }
            }
        }

        public void AddComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Comment (RoutineId, UsersId, Text, CreateDateTime) 
                                                    OUTPUT INSERTED.ID
                                                    VALUES (@RoutineId, @UsersId, @Text, @CreateDateTime)";

                    cmd.Parameters.AddWithValue("@RoutineId", comment.RoutineId);
                    cmd.Parameters.AddWithValue("@UsersId", comment.UsersId);
                    cmd.Parameters.AddWithValue("@Text", comment.Text);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);


                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }



        public void DeleteComment(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        @"
                        DELETE FROM Comment
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void UpdateComment(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment
                        SET
                        [RoutineId] = @routineId,
                         [UsersId] = @usersId,
                         [Text] = @text,
                         [CreateDateTime] = @createDateTime
                        WHERE Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", comment.Id);
                    cmd.Parameters.AddWithValue("@routineId", comment.RoutineId);
                    cmd.Parameters.AddWithValue("@usersId", comment.UsersId);
                    cmd.Parameters.AddWithValue("@text", comment.Text);
                    cmd.Parameters.AddWithValue("@createDateTime", comment.CreateDateTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public Comment GetCommentById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT c.Id, c.RoutineId, c.UserId, c.Text, c.CreateDateTime,
                        r.Intention AS RoutineIntention, r.Cycles AS RoutineCycles,
                        u.DisplayName AS UsersDisplayName
                        FROM Comment c
                        LEFT JOIN Routine r ON c.RoutineId = r.Id
                        LEFT JOIN Users u ON c.UserId = u.Id
                        WHERE c.Id = @commentId";

                    cmd.Parameters.AddWithValue("@commentId", id);
                    var reader = cmd.ExecuteReader();

                    Comment comment = null;
                    if (reader.Read())
                    {
                        //Create new comment object and populate its properties
                        comment = new Comment();
                        
                            comment.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            comment.RoutineId = reader.GetInt32(reader.GetOrdinal("RoutineId"));
                            comment.UsersId = reader.GetInt32(reader.GetOrdinal("UserId"));
                            comment.Text = reader.GetString(reader.GetOrdinal("Text"));
                            comment.CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"));

                            comment.Routine = new Routine
                            {
                                Intention = reader.GetString(reader.GetOrdinal("RoutineIntention")),
                                Cycles = reader.GetInt32(reader.GetOrdinal("RoutineCycles"))
                            };

                            comment.Users = new Users
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("UserDisplayName"))
                            };
                        }

                    reader.Close();

                    return comment;
                    }
                }
            }
        }
    }
