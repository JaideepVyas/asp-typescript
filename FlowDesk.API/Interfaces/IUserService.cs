using FlowDesk.API.DTOs.Users;

namespace FlowDesk.API.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto);

    Task<List<UserResponseDto>> GetAllUsersAsync();
    Task<string> LoginAsync(LoginUserDto loginUserDto);
}