
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration)
                .AddBasketModule(builder.Configuration)
                .AddOrderModule(builder.Configuration);

builder.Services.AddCarter(configurator: cnfg =>
{
    var catalog = typeof(CatalogModule).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(ICarterModule))).ToArray();
    cnfg.WithModules(catalog);
});

var app = builder.Build();

app.MapCarter();

app.UseCatalogModule()
    .UseOrderModule()
    .UseBasketlogModule()
;

app.Run();
