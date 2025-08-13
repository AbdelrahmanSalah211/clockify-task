using Microsoft.AspNetCore.Mvc;
using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ClockifyTask.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPatch("{userId:int}")]
        public async Task<ActionResult<UserDto>> Update(int userId, UpdateUserDto userDto)
        {
            var updatedUser = await _userService.UpdateAsync(userId, userDto);
            return Ok(updatedUser);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
