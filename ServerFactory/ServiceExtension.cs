using DataAccess;
using DataAccess.Interfaces;
using Logic;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromotionStrategies;

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
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();


        services.AddScoped<IProductLogic, ProductLogic>();
        services.AddScoped<IUserLogic, UserLogic>();
        services.AddScoped<ISessionTokenLogic, SessionTokenLogic>();
        services.AddScoped<IPurchaseLogic, PurchaseLogic>();
        services.AddScoped<IPromotionLogic, PromotionLogic>();
        services.AddScoped<IColorLogic, ColorLogic>();
        services.AddScoped<ICategoryLogic, CategoryLogic>();
        services.AddScoped<IBrandLogic, BrandLogic>();

        services.AddScoped<IPromotionStrategy, TwentyPercentPromotionStrategy>();
        services.AddScoped<IPromotionStrategy, FidelityPromotionStrategy>();
        services.AddScoped<IPromotionStrategy, ThreeForTwoPromotionStrategy>();
        services.AddScoped<IPromotionStrategy, TotalLookPromotionStrategy>();
        return services;
    }
}