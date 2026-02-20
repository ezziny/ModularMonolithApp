using System.Reflection;
using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.SharedExtensions;

public static class CarterExtensions
{
    public static IServiceCollection AddCarterAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddCarter(configurator: cnfg =>
        {
        foreach (var assembly in assemblies)
        {
            var modules = assembly.GetTypes().Where(a => a.IsAssignableTo(typeof(ICarterModule))).ToArray();
            cnfg.WithModules(modules);
        }
        });
        return services;
        }
}
