using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using UserServiceManagement.Contracts.Services;
using UserServiceManagement.Models.Models;

namespace UserService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("createuser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
            return CreatedAtAction(nameof(GetUserById), new { id = userDto.FirstName }, userDto);
        }

        [HttpGet]
        [Route("getallusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("getuserbyemail={userEmail}")]
        public async Task<ActionResult<User>> GetUserById(string userEmail)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("deleteuser={useremail}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return NoContent();
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
