using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Services;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SeoulAir.Command.Domain.Services
{
    public class NotificationChannelService : IChannelService<NotificationDto>
    {
        private readonly Channel<NotificationDto> _notificationChannel;

        public NotificationChannelService()
        {
            _notificationChannel = Channel.CreateUnbounded<NotificationDto>();
        }

        public bool TryRead(out NotificationDto notification)
        {
            return _notificationChannel.Reader.TryRead(out notification);
        }

        public async ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default)
        {
            return await _notificationChannel.Reader.WaitToReadAsync(cancellationToken);
        }

        public async ValueTask WiteAsync(NotificationDto notification, CancellationToken cancellationToken = default)
        {
            await _notificationChannel.Writer.WriteAsync(notification, cancellationToken);
        }
    }
}
