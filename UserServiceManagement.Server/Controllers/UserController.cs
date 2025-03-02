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
        public async Task<IActionResult> CreateUser([FromBody]  CreateUserRequest request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                ProfilePictureUrl = request.ProfilePictureUrl,
                PasswordHash = request.Password
            };

            await _userService.AddUser(user);

            return Ok();
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

        [HttpPost]
        [Route("loginuser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var result = await _userService.LoginUser(request);

            return Ok(result);
        }
    }
}
