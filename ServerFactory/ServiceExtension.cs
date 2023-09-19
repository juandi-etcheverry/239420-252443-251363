using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServerFactory;


public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext, Context>(o => o.UseSqlServer(connectionString));
        return services;
    }
}