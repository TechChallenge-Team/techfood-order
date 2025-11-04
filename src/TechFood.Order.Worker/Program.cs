using Microsoft.AspNetCore.Builder;
using TechFood.Order.Application;
using TechFood.Order.Infra;

var builder = Host.CreateApplicationBuilder(args);
{
    builder.Services.AddWorker();
    builder.Services.AddApplication();
    builder.Services.AddInfra();
}

var app = builder.Build();
{
    app.Run();
}
