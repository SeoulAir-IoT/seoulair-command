using System;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Dtos;

namespace SeoulAir.Command.Domain.Interfaces.Services
{
    public interface ICommandService
    {
        Task<CommandDto> FindAsync(string id);
        Task<CommandDto> FindByNameAsync(string name);
        Task<string> AddAsync(CommandDto course);
        Task DeleteAsync(string id);
        Task<PaginatedResultDto<CommandDto>> GetPaginated(Paginator paginator);
        void Update(string id, CommandDto dto);
    }
}
