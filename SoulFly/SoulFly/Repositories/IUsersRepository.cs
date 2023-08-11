using SoulFly.Models;
using SoulFly.Repositories;

namespace SoulFly.Repositories
{
    public interface IUsersRepository
    {
        void Add(Users users);
        List<Users> GetAllUsers();
        Users GetByEmail(string email);
        Users GetUsersById(int id);
        void Update(Users users);
    }
}