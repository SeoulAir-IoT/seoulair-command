using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Api.Configuration.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(string.Format(SwaggerEndpoint, OpenApiInfoProjectVersion),
                    OpenApiInfoProjectName);
                config.RoutePrefix = string.Empty;
                config.DocumentTitle = OpenApiInfoTitle;
            });

            return app;
        }
        
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            var context = app.ApplicationServices.GetService(typeof(T));
            (context as T)?.Database.Migrate();
        }
    }
}
