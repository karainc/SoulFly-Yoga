using Microsoft.Extensions.Hosting;
using server_yoga.Models;
using server_yoga.Repositories;
using server_yoga.Utils;

namespace server_yoga.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetAllByRoutineId(int routineId)
        {
            List<Comment> comments = new List<Comment>();

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT c.Id, 
                    c.RoutineId, c.UsersId, c.Text, c.CreateDateTime,
                    r.Intention AS RoutineIntention, r.Cycles AS RoutineCycles,
                    p.Name AS PoseName
                    u.DisplayName AS UsersDisplayName
                    FROM Comment c
                    JOIN Routine r ON c.RoutineId = p.Id
                    LEFT JOIN Users u ON c.UsersId = u.Id
                    LEFT JOIN Poses p ON r.PoseId = p.Id
                    WHERE c.RoutineId = @RoutineId
                    ORDER BY c.CreateDateTime DESC";

                    cmd.Parameters.AddWithValue("@RoutineId", routineId);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Comment comment = new Comment();

                        comment.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        comment.RoutineId = reader.GetInt32(reader.GetOrdinal("routineId"));
                        comment.UsersId = reader.GetInt32(reader.GetOrdinal("usersId"));
                        comment.Text = reader.GetString(reader.GetOrdinal("text"));
                        comment.CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createDateTime)"));

                        comment.Routine = new Routine
                        {
                            Intention = reader.GetString(reader.GetOrdinal("RoutineIntention")),
                            Cycles = reader.GetInt32(reader.GetOrdinal("RoutineCycles")),
                        };

                        comment.Users = new Users
                        {
                            DisplayName = reader.GetString(reader.GetOrdinal("UsersDisplayName"))
                        };

                        comment.Poses = new Poses
                        {
                            Name = reader.GetString(reader.GetOrdinal("PosesName"))
                        };
                        comments.Add(comment);
                    }
                }
            }
            return comments;
        }

        public void Add(Comment comment)
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
                        SELECT c.Id, c.RoutineId, c.UsersId, c.Text, c.CreateDateTime,
                        r.Intention AS RoutineIntention, r.Cycles AS RoutineCycles,
                        u.DisplayName AS UsersDisplayName
                        FROM Comment c
                        LEFT JOIN Routine r ON c.RoutineId = r.Id
                        LEFT JOIN Users u ON c.UsersId = u.Id
                        WHERE c.Id = @commentId";

                    cmd.Parameters.AddWithValue("@commentId", id);
                    var reader = cmd.ExecuteReader();

                    Comment comment = null;
                    if (reader.Read())
                    {
                        //Create new comment object and populate its properties
                        comment = new Comment();
                        {
                            comment.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            comment.RoutineId = reader.GetInt32(reader.GetOrdinal("RoutineId"));
                            comment.UsersId = reader.GetInt32(reader.GetOrdinal("UsersId"));
                            comment.Text = reader.GetString(reader.GetOrdinal("Text"));
                            comment.CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"));

                            comment.Routine = new Routine
                            {
                                Intention = reader.GetString(reader.GetOrdinal("RoutineIntention")),
                                Cycles = reader.GetInt32(reader.GetOrdinal("RoutineCycles"))
                            };

                            comment.Users = new Users
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("UsersDisplayName"))
                            };
                        }
                    }

                    reader.Close();

                    return comment;
                }
            }
        }
    }
}