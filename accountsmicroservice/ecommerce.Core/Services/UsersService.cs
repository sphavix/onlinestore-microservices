using AutoMapper;
using ecommerce.Core.Abstractions;
using ecommerce.Core.Dtos;
using ecommerce.Core.Models;

namespace ecommerce.Core.Services;
internal class UsersService(
    IUsersRepository repository,
    IMapper mapper) : IUsersService
{
    private readonly IUsersRepository _repository = repository;
    private readonly IMapper _mapper = mapper;  

    public async Task<AuthResponse?> Login(LoginRequest request)
    {
        var user = await _repository.GetUserByEmailAndPasswordAsync(request.Email, request.Password);

        if(user == null)
        {
            return null; // User not found or invalid credentials
        }

        return _mapper.Map<AuthResponse>(user) 
            with { Success = true, Token = "token" };
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

        return _mapper.Map<AuthResponse>(user)
            with { Success = true, Token = "token" };
    }
}
