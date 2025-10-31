using Microsoft.EntityFrameworkCore;
using TechFood.Application;
using TechFood.Infra;
using TechFood.Infra.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfra();
}

var app = builder.Build();
{
    //Run migrations
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
        dataContext.Database.Migrate();
    }

    app.UsePathBase("/api");

    app.UseForwardedHeaders();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
        app.UseHttpsRedirection();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseSwagger(options =>
        {
            options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
        });

        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();

    app.UseInfra();

    app.UseHealthChecks("/health");

    app.UseRouting();

    app.UseCors();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
