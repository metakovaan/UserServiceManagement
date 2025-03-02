using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string userEmail);
        Task<bool> DeleteUserAsync(string email);
        Task<bool> UpdateUserAsync(User user);
    }
}
