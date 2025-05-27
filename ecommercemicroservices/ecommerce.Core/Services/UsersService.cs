using ecommerce.Core.Abstractions;
using ecommerce.Core.Dtos;
using ecommerce.Core.Models;

namespace ecommerce.Core.Services;
internal class UsersService(
    IUsersRepository repository) : IUsersService
{
    private readonly IUsersRepository _repository = repository;

    public async Task<AuthResponse?> Login(LoginRequest request)
    {
        var user = await _repository.GetUserByEmailAndPasswordAsync(request.Email, request.Password);

        if(user == null)
        {
            return null; // User not found or invalid credentials
        }

        return new AuthResponse(
            user.UserId, 
            user.Email, 
            user.Password, 
            user.FullName, 
            user.Gender, 
            "token", 
            Success: true);
    }

    public async Task<AuthResponse?> Register(RegisterRequest request)
    {
        ApplicationUser user = new()
        {
            Email = request.Email,
            Password = request.Password,
            FullName = request.FullName,
            Gender = request.Gender.ToString()
        };

        // Check if user already exists
        var registeredUser = await _repository.CreateUserAsync(user);

        if (registeredUser is null)
        {
            return null;
        }

        return new AuthResponse(
            user.UserId,
            user.Email,
            user.Password,
            user.FullName,
            user.Gender,
            "token",
            Success: true);
    }
}
