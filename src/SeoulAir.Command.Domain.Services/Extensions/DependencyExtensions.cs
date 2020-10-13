using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Services;
using SeoulAir.Command.Domain.Options;

namespace SeoulAir.Command.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddHostedService<MqttSender<NotificationDto, MqttNotificationsConnectionOptions>>();

            services.AddSingleton<IChannelService<NotificationDto>,
                NotificationChannelService>();

            return services;
        }
    }
}
