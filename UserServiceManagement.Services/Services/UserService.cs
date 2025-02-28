using UserServiceManagement.Contracts.Repositories;
using UserServiceManagement.Contracts.Services;
using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
