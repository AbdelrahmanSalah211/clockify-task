using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Name))
            {
                return BadRequest("Invalid user data.");
            }

            var createdUser = await _userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(Create), new { id = createdUser.Id }, createdUser);
        }
    }
}
