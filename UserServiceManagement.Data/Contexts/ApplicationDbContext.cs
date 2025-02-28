using Microsoft.EntityFrameworkCore;
using UserServiceManagement.Models.Models;

namespace UserServiceManagement.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
