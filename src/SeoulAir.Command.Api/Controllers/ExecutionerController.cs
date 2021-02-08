using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Services;

namespace SeoulAir.Command.Api.Controllers
{
    /// <summary>
    /// Acts as console input to execute command provided by this microservice.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class ExecutionerController : ControllerBase
    {
        private readonly IExecutionerService _executionerService;

        public ExecutionerController(IExecutionerService executionerService)
        {
            _executionerService = executionerService;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="request">Request with execution parameters. Command name from database and it's parameters</param>
        [HttpPut("execute")]
        public async Task<IActionResult> Execute(ExecutionRequest request)
        {
            var response = await _executionerService.ExecuteCommand(request);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent))
                return StatusCode((int)response.StatusCode);

            ObjectResult result = new ObjectResult(responseContent)
            {
                StatusCode = (int) response.StatusCode
            };

            return result;
        }
    }
}
