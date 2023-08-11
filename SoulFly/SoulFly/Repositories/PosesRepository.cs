using SoulFly.Models;
using SoulFly.Utils;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using SoulFly.Repositories;


namespace SoulFly.Repositories
{
    public class PosesRepository : BaseRepository, IPosesRepository
    {
        public PosesRepository(IConfiguration config) : base(config) { }

        public List<Poses> GetAllPoses()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name, Description, Image FROM Poses ORDER BY Name ASC";

                    var reader = cmd.ExecuteReader();

                    var poses = new List<Poses>();

                    while (reader.Read())
                    {
                       poses.Add(new Poses()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            Image = reader.GetString(reader.GetOrdinal("Image"))
                        });

                    }
                    reader.Close();

                    return poses;
                }
            }
        }

        //get by id
        public Poses GetPosesById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                    SELECT Id, [Name], [Description], [Image]
                    FROM Poses
                    WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    Poses poses = null;
                    if (reader.Read())
                    {
                        //Create new poses object and populate its properties
                        poses = new Poses()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            Image = DbUtils.GetString(reader, "Image"),

                        };
                    }

                    reader.Close();

                    return poses;
                }
            }
        }
    }
}