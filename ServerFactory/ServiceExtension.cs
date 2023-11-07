using DataAccess;
using DataAccess.Interfaces;
using Logic;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ServerFactory;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext, Context>(o => o.UseSqlServer(connectionString));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();


        services.AddScoped<IProductLogic, ProductLogic>();
        services.AddScoped<IUserLogic, UserLogic>();
        services.AddScoped<ISessionTokenLogic, SessionTokenLogic>();
        services.AddScoped<IPurchaseLogic, PurchaseLogic>();
        services.AddScoped<IPromotionLogic, PromotionLogic>();
        services.AddScoped<IFileDataReader, FileDataReader>();
        return services;
    }
}