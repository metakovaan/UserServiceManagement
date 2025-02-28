using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Contracts.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<User> GetUserByEmail(string userEmail);
        Task<IEnumerable<User>> GetAllUsers();
        Task DeleteUser(string userEmail);
    }
}
