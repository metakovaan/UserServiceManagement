using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Contracts.Services
{
    public interface IUserService
    {
        Task<ServiceResult> AddUser(CreateUserRequest request);
        Task<ServiceResult> GetUserByEmail(string userEmail);
        Task<ServiceResult> DeleteUser(string userEmail);
        Task<ServiceResult> LoginUser(LoginUserRequest request);
    }
}
