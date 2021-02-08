using System;
using System.Net.Http;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Builders;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Repositories;
using SeoulAir.Command.Domain.Interfaces.Services;
using SeoulAir.Command.Domain.Options;
using static SeoulAir.Command.Domain.Resources.Strings;

namespace SeoulAir.Command.Domain.Services
{
    public class ExecutionerService : IExecutionerService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMicroserviceUriBuilder _uriBuilder;
        private readonly IMicroserviceHttpRequestBuilder _requestBuilder;
        private readonly IHttpClientFactory _clientFactory;

        public ExecutionerService(ICommandRepository commandRepository, IMicroserviceUriBuilder uriBuilder,
            IMicroserviceHttpRequestBuilder requestBuilder, IHttpClientFactory clientFactory)
        {
            _commandRepository = commandRepository;
            _uriBuilder = uriBuilder;
            _requestBuilder = requestBuilder;
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> ExecuteCommand(ExecutionRequest request)
        {
            var commandToExecute = await _commandRepository.FindByNameAsync(request.CommandName);

            if (commandToExecute == default)
                throw new ArgumentException(CommandDoesNotExistException);

            var uri = _uriBuilder.Restart()
                .UseMicroserviceUrlOptions(new MicroserviceUrlOptions
                {
                    Address = commandToExecute.Address,
                    Port = commandToExecute.Port
                }).SetEndpoint(commandToExecute.Endpoint)
                .UseController(commandToExecute.Controller);

            foreach (var requestParameter in request.Parameters)
                uri.AddPathParameter(requestParameter);

            var httpRequest = _requestBuilder.Restart()
                .UseHttpMethod(new HttpMethod(commandToExecute.HttpMethod))
                .UseUri(uri.Build())
                .Build();

            using var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(httpRequest);

            return response;
        }
    }
}
