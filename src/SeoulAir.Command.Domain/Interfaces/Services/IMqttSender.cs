using SeoulAir.Command.Domain.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace SeoulAir.Command.Domain.Interfaces.Services
{
    public interface IMqttSender<TNotificationDto>
        where TNotificationDto : NotificationDto
    {
        Task<bool> Send(TNotificationDto message, CancellationToken cancellationToken = default);
    }
}
