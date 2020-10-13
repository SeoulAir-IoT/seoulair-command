using SeoulAir.Command.Domain.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace SeoulAir.Command.Domain.Interfaces.Services
{
    public interface IChannelService<TNotificationDto>
        where TNotificationDto : NotificationDto
    {
        ValueTask WiteAsync(TNotificationDto notification, CancellationToken cancellationToken = default);
        ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default);
        bool TryRead(out TNotificationDto notification);
    }
}
