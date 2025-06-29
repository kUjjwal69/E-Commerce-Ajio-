using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string email, string password);
        Task<User> Register(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
    }

}
