using System.Net.Http;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Dtos;

namespace SeoulAir.Command.Domain.Interfaces.Services
{
    public interface IExecutionerService
    {
        Task<HttpResponseMessage> ExecuteCommand(ExecutionRequest request);
    }
}