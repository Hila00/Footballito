using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Footballito.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Data Source=footballito.db";
        
        services.AddDbContext<FootballContext>(options =>
            options.UseSqlite(connectionString));
    }
}