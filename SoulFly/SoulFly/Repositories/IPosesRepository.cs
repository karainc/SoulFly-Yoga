using SoulFly.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace SoulFly.Repositories
{
    public interface IPosesRepository
    {
     
        List<Poses> GetAllPoses();
       
        Poses GetPosesById(int id);
 
    }
}