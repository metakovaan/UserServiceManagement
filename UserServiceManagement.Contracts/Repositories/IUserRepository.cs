using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserByEmail(string userEmail);
        Task<List<User>> GetAllUsers();
        Task<bool> DeleteUser(string userEmail);
    }
}
