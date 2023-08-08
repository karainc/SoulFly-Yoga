using server_yoga.Models;
using server_yoga.Utils;


namespace server_yoga.Repositories
{
    public class PosesRepository : BaseRepository, IPosesRepository
    {
        private readonly List<Poses> poses;

        public PosesRepository(IConfiguration config) : base(config) { }

        public List<Poses> GetAllPoses()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Poses.Id, 
                    Poses.Name, Poses.Description, Poses.Image,
                    FROM [Poses]                
                    ORDER BY Name";

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


        public Poses GetPosesById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT Id, 
                    Name, Description, Image,
                    FROM Poses
                    ORDER BY NAME";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Poses poses = null;
                    if (reader.Read())
                    {
                        //Create new poses object and populate its properties
                        poses = new Poses();
                        {
                            poses.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            poses.Name = reader.GetString(reader.GetOrdinal("Name"));
                            poses.Description = reader.GetString(reader.GetOrdinal("Description"));
                            poses.Image = reader.GetString(reader.GetOrdinal("Image"));

                        }
                    }

                    reader.Close();

                    return poses;
                }
            }
        }
    }
}