using Microsoft.AspNetCore.Http;

namespace UserServiceManagement.Models.Models
{
    public class CreateUserRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public IFormFile FormFile { get; set; }
        public required string Password { get; set; }
    }
}
