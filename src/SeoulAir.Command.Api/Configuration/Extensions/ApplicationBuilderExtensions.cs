using Microsoft.AspNetCore.Builder;
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
                config.SwaggerEndpoint(SwaggerEndpoint, OpenApiInfoProjectName);
                config.RoutePrefix = string.Empty;
                config.DocumentTitle = OpenApiInfoTitle;
            });

            return app;

        }
    }
}
