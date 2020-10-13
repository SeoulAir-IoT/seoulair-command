using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Extensions;
using SeoulAir.Command.Domain.Interfaces.Services;
using SeoulAir.Command.Domain.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Domain.Services
{
    public class MqttSender<TNotificationDto, TOptionsDto> : BackgroundService, IMqttSender<TNotificationDto>
        where TNotificationDto : NotificationDto
        where TOptionsDto : MqttConnectionOptions, new()
    {
        private readonly IChannelService<TNotificationDto> _messageChannel;
        private readonly ILogger<MqttSender<TNotificationDto, TOptionsDto>> _logger;
        private readonly TOptionsDto _brokerOptions;

        protected MqttSender(IChannelService<TNotificationDto> messageChannel,
                          IOptions<TOptionsDto> brokerOptions,
                          ILogger<MqttSender<TNotificationDto, TOptionsDto>> logger)
        {
            _messageChannel = messageChannel;
            _logger = logger;
            _brokerOptions = brokerOptions.Value;
        }        

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(string.Format(MqttSenderStartingMessage, typeof(TNotificationDto)));
            return BackgroundProcessing(stoppingToken);
        }
        
        public async Task<bool> Send(TNotificationDto message, CancellationToken cancellationToken = default)
        {
            if (message == null)
            {
                ArgumentNullException exception = new ArgumentNullException(nameof(message));
                _logger.LogError(exception.ToString().FormatAsExceptionMessage());
                throw exception;
            }

            bool isSent = false;
            byte currentAttempt = 0;
            while (!cancellationToken.IsCancellationRequested &&
                currentAttempt < _brokerOptions.MaxNumOfAttemptsToSendMessage &&
                !isSent)
            {
                try
                {
                    await SendMessage(message);
                    isSent = true;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.ToString().FormatAsExceptionMessage());
                    currentAttempt++;
                    if (currentAttempt >= _brokerOptions.MaxNumOfAttemptsToSendMessage)
                        return false;
                }
            }
            return true;
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
                if (await _messageChannel.WaitToReadAsync(cancellationToken))
                    while (_messageChannel.TryRead(out var messageToSed) && !cancellationToken.IsCancellationRequested)
                        await Send(messageToSed);
        }

        private async Task SendMessage(TNotificationDto message)
        {
            using (IMqttClient client = await CreateMqttClientConnection())
            {
                string jsonObject = JsonSerializer.Serialize<TNotificationDto>(message);

                MqttApplicationMessage Message = new MqttApplicationMessageBuilder()
                    .WithTopic(_brokerOptions.Topic)
                    .WithPayload(jsonObject)
                    .WithRetainFlag()
                    .Build();

                await client.PublishAsync(Message, CancellationToken.None);
            }
        }

        private async Task<IMqttClient> CreateMqttClientConnection()
        {
            MqttFactory factory = new MqttFactory();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(_brokerOptions.BrokerAddress, _brokerOptions.BrokerPort)
                .WithClientId(MqttSenderClientId)
                .Build();

            IMqttClient client = factory.CreateMqttClient();
            await client.ConnectAsync(options);

            return client;
        }
    }
}
