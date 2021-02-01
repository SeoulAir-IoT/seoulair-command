using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Command.Domain.Builders;
using SeoulAir.Command.Domain.Services.Builders;

namespace SeoulAir.Command.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IMicroserviceHttpRequestBuilder, MicroserviceHttpRequestBuilder>();
            services.AddScoped<IMicroserviceUriBuilder, MicroserviceUriBuilder>();
            return services;
        }
    }
}
