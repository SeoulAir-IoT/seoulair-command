using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SeoulAir.Command.Domain.Options;
using SeoulAir.Command.Domain.Services.OptionsValidators;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(OpenApiInfoProjectVersion, new OpenApiInfo
                {
                    Title = OpenApiInfoTitle,
                    Description = OpenApiInfoDescription,
                    Version = OpenApiInfoProjectVersion
                });
            });

            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MqttNotificationsConnectionOptions>(
                configuration.GetSection(MqttNotificationsConnectionOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MqttNotificationsConnectionOptions>,
                MqttNotificationsConnectionOptionsValidator>();

            return services;
        }
    }
}
