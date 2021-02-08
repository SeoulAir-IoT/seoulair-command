using System;
using SeoulAir.Command.Domain.Options;

namespace SeoulAir.Command.Domain.Builders
{
    public interface IMicroserviceUriBuilder
    {
        IMicroserviceUriBuilder UseMicroserviceUrlOptions(MicroserviceUrlOptions microserviceOptions);
        IMicroserviceUriBuilder UseController(string controllerName);
        IMicroserviceUriBuilder SetEndpoint(string endpoint);
        IMicroserviceUriBuilder AddQueryParameter<TParameter>(string parameterName, TParameter value);
        IMicroserviceUriBuilder AddPathParameter(string value);
        IMicroserviceUriBuilder Restart();
        Uri Build();
    }
}
