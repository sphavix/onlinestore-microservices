using ecommerce.Core.Dtos;

namespace ecommerce.Core.Services;

public interface IUsersService
{
    Task<AuthResponse?> Login(LoginRequest request);

    Task<AuthResponse?> Register(RegisterRequest request);
}
