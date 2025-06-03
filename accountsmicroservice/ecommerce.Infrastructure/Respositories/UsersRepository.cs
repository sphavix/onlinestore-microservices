using Dapper;
using ecommerce.Core.Abstractions;
using ecommerce.Core.Models;
using ecommerce.Infrastructure.Persistence;

namespace ecommerce.Infrastructure.Respositories;
internal class UsersRepository(DbContextConfigurations context) : IUsersRepository
{
    private readonly DbContextConfigurations _context = context;
    public async Task<ApplicationUser?> GetUserByEmailAndPasswordAsync(string? email, string? password)
    {
        // SQL Query to get the user by email and password with dapper
        var query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";

        var user = await _context.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, 
            new { Email = email, Password = password });

        return user; // Returns the user if found, otherwise null
    }

    public async Task<ApplicationUser?> CreateUserAsync(ApplicationUser user)
    {
        // Genereate new unique identifier for the user
        user.UserId = Guid.NewGuid();

        // SQL Query to insert the user into the database with dapper
        var command = "INSERT INTO public.\"Users\" (\"UserId\", \"Email\", \"Password\", \"FullName\", \"Gender\") " +
                       "VALUES (@UserId, @Email, @Password, @FullName, @Gender)";

        int rowCount = await _context.DbConnection.ExecuteAsync(command, user);

        if(rowCount > 0)
        {
            return user;
        }
        else
        {
            return null;
        }
    }
}
