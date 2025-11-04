using Microsoft.AspNetCore.Builder;

namespace TechFood.Order.Infra
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfra(this IApplicationBuilder app)
        {
            app.UseSharedInfra();

            return app;
        }
    }
}
