using Microsoft.Extensions.Options;
using SeoulAir.Command.Domain.Options;

namespace SeoulAir.Command.Domain.Services.OptionsValidators
{
    public class MqttNotificationsConnectionOptionsValidator 
        : MqttConnectionOptionsValidator,
          IValidateOptions<MqttNotificationsConnectionOptions>
    {
        public ValidateOptionsResult Validate(string name, MqttNotificationsConnectionOptions options)
        {
            return base.Validate(name, options);
        }
    }
}
