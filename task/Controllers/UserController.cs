using Microsoft.AspNetCore.Mvc;
using task.Models;
using task.Services;

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
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
      if (user == null || string.IsNullOrEmpty(user.Name))
      {
        return BadRequest("Invalid user data.");
      }

      var createdUser = await _userService.CreateUserAsync(user);
      return CreatedAtAction(nameof(CreateUser), new { id = createdUser.Id }, createdUser);
    }
  }
}