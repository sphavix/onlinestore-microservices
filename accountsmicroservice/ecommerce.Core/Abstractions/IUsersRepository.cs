using ecommerce.Core.Models;

namespace ecommerce.Core.Abstractions;
public interface IUsersRepository
{
    Task<ApplicationUser?> CreateUserAsync(ApplicationUser user);

    Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string? email, string? password);
}
