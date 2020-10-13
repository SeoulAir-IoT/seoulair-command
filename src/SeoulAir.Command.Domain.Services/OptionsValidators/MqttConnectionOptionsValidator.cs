using Microsoft.Extensions.Options;
using SeoulAir.Command.Domain.Extensions;
using SeoulAir.Command.Domain.Options;
using System.Collections.Generic;
using System.Linq;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Domain.Services.OptionsValidators
{
    public class MqttConnectionOptionsValidator : IValidateOptions<MqttConnectionOptions>
    {
        private const int PortMinNumber = 1024;
        private const int PortMaxNumber = 65535;
        private const int NumOfAttemptsUpperBoundry = 15;
        private const int NumOfAttemptsDownBoundry = 0;

        public ValidateOptionsResult Validate(string name, MqttConnectionOptions options)
        {
            ICollection<string> failureMessages = new List<string>();

            if (string.IsNullOrEmpty(options.BrokerAddress))
                failureMessages.Add(string.Format(
                    ParameterNullOrEmptyMessage,
                    nameof(options.BrokerAddress)).FormatAsExceptionMessage());

            if (string.IsNullOrEmpty(options.Topic))
                failureMessages.Add(string.Format(
                    ParameterNullOrEmptyMessage,
                    nameof(options.Topic)).FormatAsExceptionMessage());

            if (options.BrokerPort < PortMinNumber || options.BrokerPort > PortMaxNumber)
                failureMessages.Add(string.Format(
                    ParameterBetweenMessage,
                    nameof(options.BrokerPort),
                    PortMinNumber,
                    PortMaxNumber));

            if (options.MaxNumOfAttemptsToSendMessage < NumOfAttemptsDownBoundry 
                || options.MaxNumOfAttemptsToSendMessage > NumOfAttemptsUpperBoundry)
                failureMessages.Add(string.Format(
                    ParameterBetweenMessage,
                    nameof(options.MaxNumOfAttemptsToSendMessage),
                    NumOfAttemptsDownBoundry,
                    NumOfAttemptsUpperBoundry));

            if (failureMessages.Any())
                return ValidateOptionsResult.Fail(failureMessages);

            return ValidateOptionsResult.Success;
        }
    }
}
