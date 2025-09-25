using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app )
        {
            using var Scoope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedingAsync();
            await ObjectOfDataSeeding.IdentityDataSeedingAsync();
        }
        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app) 
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWare(this IApplicationBuilder app) 
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
