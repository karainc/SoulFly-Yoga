using SoulFly.Models;
using SoulFly.Utils;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace SoulFly.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration) { }

        public List<Users> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                        SELECT Id, [DisplayName], Birthday, Password, 
                               Email 
                             
                        FROM [Users]
                        ORDER BY [DisplayName] ASC";


                    var reader = cmd.ExecuteReader();

                    var users = new List<Users>();
                    while (reader.Read())
                    {
                        users.Add(new Users()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Password = DbUtils.GetString(reader, "Password"),
                            Birthday = DbUtils.GetString(reader, "Birthday")

                        });
                    }

                    reader.Close();

                    return users;
                }
            }
        }

        public Users GetUsersById(int id)
        {
            Users users = null;

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [DisplayName], Birthday, Password, 
                               Email
                             
                        FROM [Users]
                      
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new Users()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Password = DbUtils.GetString(reader, "Password"),
                                Birthday = DbUtils.GetString(reader, "Birthday")

                            };
                        }
                    }
                }
            }

            return users;
        }

        public void Update(Users users)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                        UPDATE Users
                           SET DisplayName = @DisplayName,
                               Birthday = @Birthday,
                               Password = @Password,
                               Email = @Email

                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@DisplayName", users.DisplayName);
                    DbUtils.AddParameter(cmd, "@Birthday", users.Birthday);
                    DbUtils.AddParameter(cmd, "@Password", users.Password);
                    DbUtils.AddParameter(cmd, "@Email", users.Email);
                    DbUtils.AddParameter(cmd, "@Id", users.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Users GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, DisplayName, 
                               Email, Password, Birthday
                         FROM [Users]
                         WHERE Email = @email";

                    DbUtils.AddParameter(cmd, "@email", email);

                    Users users = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        users = new Users()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Password = DbUtils.GetString(reader, "Password"),
                            Birthday = DbUtils.GetString(reader, "Birthday")
                        };
                    }
                    reader.Close();

                    return users;
                }
            }
        }

        public void Add(Users users)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                      INSERT INTO [Users] ([DisplayName], Email, 
                                                  Password, Birthday)
                                      OUTPUT INSERTED.ID
                                      VALUES (@DisplayName, @Email, @Password, @Birthday)";

                    DbUtils.AddParameter(cmd, "@DisplayName", users.DisplayName);
                    DbUtils.AddParameter(cmd, "@Email", users.Email);
                    DbUtils.AddParameter(cmd, "@Password", users.Password);
                    DbUtils.AddParameter(cmd, "@Birthday", users.Birthday);

                    users.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
