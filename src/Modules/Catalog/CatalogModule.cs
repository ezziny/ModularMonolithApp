using System.Reflection;
using Catalog.Data;
using Catalog.Data.Seed;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Behaviors;
using SharedKernel.Data;
using SharedKernel.Data.Interceptors;

namespace Catalog;

static public class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services
    , IConfiguration configuration)
    {

        services.AddScoped<ISaveChangesInterceptor, AuditTimesEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventInterceptor>();

        services.AddMediatR(cfg =>{
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddDbContext<CatalogDBContext>((sp,o) =>{
        o.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
        o.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        }
        );
        services.AddScoped<IDataSeeder, CatalogDataSeeder>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder builder)
    {
        builder.MigrateAndSeed<CatalogDBContext>();
        return builder;
    }


}
