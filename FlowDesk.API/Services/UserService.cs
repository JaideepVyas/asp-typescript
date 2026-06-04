using FlowDesk.API.Data;
using FlowDesk.API.DTOs.Users;
using FlowDesk.API.Interfaces;
using FlowDesk.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowDesk.API.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == createUserDto.Email);

        if (existingUser is not null)
        {
            throw new Exception("Email already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = createUserDto.FullName,
            Email = createUserDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<List<UserResponseDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(user => new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<string> LoginAsync(LoginUserDto loginUserDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Email == loginUserDto.Email);

        if (user is null)
        {
            throw new Exception("Invalid email or password.");
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(
            loginUserDto.Password,
            user.PasswordHash
        );

        if (!isPasswordValid)
        {
            throw new Exception("Invalid email or password.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}