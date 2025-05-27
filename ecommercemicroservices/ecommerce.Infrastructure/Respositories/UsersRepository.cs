using ecommerce.Core.Abstractions;
using ecommerce.Core.Dtos;
using ecommerce.Core.Models;

namespace ecommerce.Infrastructure.Respositories;
internal class UsersRepository : IUsersRepository
{
    public async  Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string? email, string? password)
    {
        return new ApplicationUser
        {
            UserId = Guid.NewGuid(),
            Email = email,
            Password = password,
            FullName = "George Fyls",
            Gender = GenderOptions.Male.ToString()
        };
    }

    public async Task<ApplicationUser?> CreateUserAsync(ApplicationUser user)
    {
        // Genereate new unique identifier for the user
        user.UserId = Guid.NewGuid();

        return user;
    }
}
