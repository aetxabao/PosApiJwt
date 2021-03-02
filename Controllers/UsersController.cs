using Microsoft.AspNetCore.Mvc;
using PosApiJwt.Interfaces;
using PosApiJwt.Requests;
using PosApiJwt.Responses;
using System.Threading.Tasks;

namespace PosApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(ServerResponse("xxxxx", "Missing login details"));
            }

            var loginResponse = await userService.Login(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest(ServerResponse(loginRequest.Username, "Invalid credentials"));
            }

            return Ok(loginResponse);
        }

        private LoginResponse ServerResponse(string to, string msg)
        {
            return new LoginResponse
            {
                Username = to,
                Token = msg
            };
        }
    }
}
