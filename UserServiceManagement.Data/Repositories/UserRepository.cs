using Microsoft.EntityFrameworkCore;
using UserServiceManagement.Contracts.Repositories;
using UserServiceManagement.Data.Contexts;
using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext DbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        public async Task AddUser(User user)
        {
            await DbContext.Users.AddAsync(user);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(string userEmail)
        {
            var user = await DbContext.Users.FindAsync(userEmail);
            if (user == null)
            {
                return false;
            }
              
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();

            return true;
        }

        public Task<List<User>> GetAllUsers()
        {
            return DbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string userEmail)
        {
            var user =  await DbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(userEmail));
            if (user is not null)
            {
                return user;
            }

            return new User();
        }
    }
}
