using ecommerce.Core.Dtos;
using ecommerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IUsersService usersService) : ControllerBase
    {
        private readonly IUsersService _usersService = usersService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid login request.");
            }

            var response = await _usersService.Login(request);
            if (response == null || response.Success == false)
            {
                return Unauthorized();
            }
            return Ok(response);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid registration request.");
            }

            var response = await _usersService.Register(request);
            if (response == null || response.Success == false)
            {
                return BadRequest(request);
            }
            return Ok(response);
        }
    }
}
