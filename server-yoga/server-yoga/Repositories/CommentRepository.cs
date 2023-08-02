using Microsoft.Extensions.Hosting;
using server_yoga.Models;
using server_yoga.Repositories;

namespace server_yoga.Repositories
{
    public class CommentRepository : BaseRepository
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
                    SELECT c.Id, c.RoutineId, c.UsersId, c.Text,
                    r.Intention AS RoutineIntention, r.Cycles AS RoutineCycles, p.ImageLocation AS RoutineImageLocation,
                    u.DisplayName AS UsersDisplayName
                    FROM Comment c
                    LEFT JOIN Routine p ON c.RoutineId = p.Id
                    LEFT JOIN Users u ON c.UsersId = u.Id
                    WHERE c.RoutineId = @RoutineId
                    order by c.CreateDateTime desc";

                    cmd.Parameters.AddWithValue("@RoutineId", routineId);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Comment comment = new Comment();
                        comment.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        comment.RoutineId = reader.GetInt32(reader.GetOrdinal("routineId"));
                        comment.UsersId = reader.GetInt32(reader.GetOrdinal("usersId"));
                        comment.Text = reader.GetString(reader.GetOrdinal("text"));

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
                    cmd.CommandText = @"INSERT INTO Comment (RoutineId, UsersId, Text, Content, CreateDateTime) 
                                                    OUTPUT INSERTED.ID
                                                    VALUES (@RoutineId, @UsersId, @Text, @Content, @CreateDateTime)";

                    cmd.Parameters.AddWithValue("@RoutineId", comment.RoutineId);
                    cmd.Parameters.AddWithValue("@UsersId", comment.UsersId);
                    cmd.Parameters.AddWithValue("@Text", comment.Text);
                    cmd.Parameters.AddWithValue("@Content", comment.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", comment.CreateDateTime);


                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }



        public void Delete(int id)
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



        public void Update(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment
                        SET
                        [RoutineId] = @postId,
                         [UsersId] = @usersId,
                         [Text] = @subject,
                         [Content] = @content,
                         [CreateDateTime] = @createDateTime
                        WHERE Id = @id
                        ";
                    cmd.Parameters.AddWithValue("@id", comment.Id);
                    cmd.Parameters.AddWithValue("@postId", comment.RoutineId);
                    cmd.Parameters.AddWithValue("@usersId", comment.UsersId);
                    cmd.Parameters.AddWithValue("@subject", comment.Text);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
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
                        SELECT c.Id, c.RoutineId, c.UsersId, c.Text,, c.CreateDateTime,
                        p.Title AS RoutineTitle, p.Content AS RoutineContent, p.ImageLocation AS RoutineImageLocation,
                        u.DisplayName AS UsersDisplayName
                        FROM Comment c
                        LEFT JOIN Routine p ON c.RoutineId = p.Id
                        LEFT JOIN Users u ON c.UsersId = u.Id
                        WHERE c.Id = @commentId";

                    cmd.Parameters.AddWithValue("@commentId", id);
                    var reader = cmd.ExecuteReader();

                    Comment comment = null;

                    if (reader.Read())
                    {
                        comment = new Comment();

                        comment.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        comment.RoutineId = reader.GetInt32(reader.GetOrdinal("RoutineId"));
                        comment.UsersId = reader.GetInt32(reader.GetOrdinal("UsersId"));
                        comment.Text = reader.GetString(reader.GetOrdinal("Text"));
                        comment.Content = reader.GetString(reader.GetOrdinal("Content"));
                        comment.CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"));

                        comment.Routine = new Routine
                        {
                            Title = reader.GetString(reader.GetOrdinal("RoutineTitle")),
                            Content = reader.GetString(reader.GetOrdinal("RoutineContent")),
                            ImageLocation = reader.GetString(reader.GetOrdinal("RoutineImageLocation"))
                        };

                        comment.Users = new Users
                        {
                            DisplayName = reader.GetString(reader.GetOrdinal("UsersDisplayName"))
                        };
                    }

                    reader.Close();

                    return comment;
                }
            }
        }






    }
}