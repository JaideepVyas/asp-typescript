using FlowDesk.API.DTOs.Users;
using FlowDesk.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace FlowDesk.API.Controllers;

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
    public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto createUserDto)
    {
        try
        {
            var createdUser = await _userService.CreateUserAsync(createUserDto);

            return Ok(createdUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

   [Authorize]
[HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUserDto loginUserDto)
    {
        try
        {
            var token = await _userService.LoginAsync(loginUserDto);

            return Ok(new
            {
                token
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }
}