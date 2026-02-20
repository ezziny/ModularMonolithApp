
using Carter;
using SharedKernel.SharedExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration)
                .AddBasketModule(builder.Configuration)
                .AddOrderModule(builder.Configuration);

builder.Services.AddCarterAssemblies(
typeof(CatalogModule).Assembly,
 typeof(BasketModule).Assembly,
  typeof(OrderModule).Assembly);

var app = builder.Build();

app.MapCarter();

app.UseCatalogModule()
    .UseOrderModule()
    .UseBasketlogModule()
;

app.Run();
