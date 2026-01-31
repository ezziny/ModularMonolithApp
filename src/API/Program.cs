
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration)
                .AddBasketModule(builder.Configuration)
                .AddOrderModule(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule()
    .UseOrderModule()
    .UseBasketlogModule()
;

app.Run();
