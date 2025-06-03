using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace ecommerce.Infrastructure.Persistence;
public class DbContextConfigurations
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DbContextConfigurations(IConfiguration configuration)
    {
        _configuration = configuration;
        string ConnectionString = _configuration.GetConnectionString("DefaultConnection")!;

        _dbConnection = new NpgsqlConnection(ConnectionString); // Npgsql is used for PostgreSQL database connections
    }

    public IDbConnection DbConnection => _dbConnection; // Expose the database connection for use in repositories


}
