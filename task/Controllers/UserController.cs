using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;
using task.DTOs;

namespace task.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
      if (dto == null || string.IsNullOrEmpty(dto.Name))
      {
        return BadRequest("Invalid user data.");
      }

      var user = new User
      {
        Name = dto.Name,
        ClockifyUserId = dto.ClockifyUserId
      };

      var createdUser = await _userService.CreateUserAsync(user);
      return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Id }, createdUser);
    }
  }
}