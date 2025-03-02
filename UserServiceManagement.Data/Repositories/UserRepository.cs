using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserServiceManagement.Contracts.Repositories;
using UserServiceManagement.Data.Contexts;
using UserServiceManagement.Models.Models;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="userEmail">Email address of the user</param>
    /// <returns>User object if found, otherwise null</returns>
    public async Task<User?> GetUserByEmailAsync(string userEmail)
    {
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            _logger.LogWarning("Attempted to retrieve user with an empty email.");
            return null;
        }

        try
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving user by email: {Email}", userEmail);
            return null;
        }
    }

    /// <summary>
    /// Adds a new user to the database.
    /// </summary>
    /// <param name="user">User object to add</param>
    /// <returns>True if successful, otherwise false</returns>
    public async Task<bool> AddUserAsync(User user)
    {
        if (user == null)
        {
            _logger.LogWarning("Attempted to add a null user.");
            return false;
        }

        try
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("User {Email} added successfully.", user.Email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding user {Email}.", user.Email);
            return false;
        }
    }

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="user">Updated user object</param>
    /// <returns>True if successful, otherwise false</returns>
    public async Task<bool> UpdateUserAsync(User user)
    {
        if (user == null)
        {
            _logger.LogWarning("Attempted to update a null user.");
            return false;
        }

        try
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("User {Email} updated successfully.", user.Email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating user {Email}.", user.Email);
            return false;
        }
    }

    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// <param name="user">User object to delete</param>
    /// <returns>True if successful, otherwise false</returns>
    public async Task<bool> DeleteUserAsync(string email)
    {
        try
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User {Email} not found or could not be deleted.", email);
                return false;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("User {Email} deleted successfully.", user.Email);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting user {Email}.", email);
            return false;
        }
    }
}
