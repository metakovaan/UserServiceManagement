using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UserServiceManagement.Contracts.Repositories;
using UserServiceManagement.Contracts.Services;
using UserServiceManagement.Models.Models;
using UserServiceManagement.Utils.Encryption;

namespace UserServiceManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _logger = logger;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<ServiceResult> AddUser(CreateUserRequest request)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(request.Password);
                var imageUrl = await UploadUserImageAsync(request.FormFile);

                var user = new User
                {
                    Email = request.Email,
                    PasswordHash = hashedPassword,
                    ProfilePictureUrl = imageUrl,
                    FirstName = request.FirstName,
                    LastName = request.LastName

                };

                var isSuccess = await _userRepository.AddUserAsync(user);

                if (!isSuccess)
                {
                    _logger.LogError("Failed to add user {Email}.", user.Email);
                    return new ServiceResult { IsSuccess = false, Message = "User could not be added." };
                }

                _logger.LogInformation("User {Email} added successfully.", user.Email);
                return new ServiceResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding user {request.Email}.");
                return new ServiceResult { IsSuccess = false, Message = "An unexpected error occurred." };
            }
        }

        public async Task<ServiceResult> DeleteUser(string userEmail)
        {
            try
            {
                var isSuccess = await _userRepository.DeleteUserAsync(userEmail);
                if (!isSuccess)
                {
                    _logger.LogWarning("User {Email} not found or could not be deleted.", userEmail);
                    return new ServiceResult { IsSuccess = false, Message = "User not found or deletion failed." };
                }

                _logger.LogInformation("User {Email} deleted successfully.", userEmail);
                return new ServiceResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user {Email}.", userEmail);
                return new ServiceResult { IsSuccess = false, Message = "An unexpected error occurred." };
            }
        }

        public async Task<ServiceResult> GetUserByEmail(string userEmail)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(userEmail);
                if (user == null)
                {
                    return new ServiceResult { IsSuccess = false, Message = "User not found." };
                }

                return new ServiceResult { IsSuccess = true, Data = user };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user {Email}.", userEmail);
                return new ServiceResult { IsSuccess = false, Message = "An unexpected error occurred." };
            }
        }

        public async Task<ServiceResult> LoginUser(LoginUserRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Invalid login attempt for email {Email}.", request.Email);
                    return new ServiceResult { IsSuccess = false, Message = "Invalid email or password." };
                }

                _logger.LogInformation("User {Email} logged in successfully.", request.Email);

                return new ServiceResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user {Email}.", request.Email);
                return new ServiceResult { IsSuccess = false, Message = "An unexpected error occurred." };
            }
        }

        private async Task<string> UploadUserImageAsync(IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();
            return await _cloudinaryService.UploadImageAsync(stream, formFile.FileName);
        }
    }
}
