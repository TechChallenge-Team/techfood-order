using TechFood.Order.Application;
using TechFood.Order.Infra;
using TechFood.Order.Infra.Persistence.Contexts;
using TechFood.Shared.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation(builder.Configuration, new PresentationOptions
    {
        AddSwagger = true,
        AddJwtAuthentication = true,
        SwaggerTitle = "TechFood Order API V1",
        SwaggerDescription = "TechFood Order API V1"
    });
    builder.Services.AddApplication();
    builder.Services.AddInfra();
}

var app = builder.Build();
{
    app.RunMigration<OrderContext>();

    app.UsePathBase("/order");

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
    }

    app.UseSwaggerUI();

    app.UseInfra();

    app.UseHealthChecks("/health");

    app.UseRouting();

    app.UseCors();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
