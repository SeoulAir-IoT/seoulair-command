using System;
using System.Net.Http;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Builders;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Repositories;
using SeoulAir.Command.Domain.Interfaces.Services;
using SeoulAir.Command.Domain.Options;

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
                throw new ArgumentException("Command with this name does not exist");

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

            HttpResponseMessage response;
            using (var client = _clientFactory.CreateClient())
                response = await client.SendAsync(httpRequest);

            return response;
        }
    }
}