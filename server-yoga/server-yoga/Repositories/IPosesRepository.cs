using server_yoga.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace server_yoga.Repositories
{
    public interface IPosesRepository
    {
     
        List<Poses> GetAllPoses();
       
        Poses GetPosesById(int id);
 
    }
}