using System;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Dtos;

namespace SeoulAir.Command.Domain.Interfaces.Repositories
{
    public interface ICommandRepository
    {
        Task<CommandDto> FindAsync(Guid id);
        Task<CommandDto> FindByNameAsync(string name);
        Task<CommandDto> AddAsync(CommandDto course);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, CommandDto dto);
        Task<PaginatedResultDto<CommandDto>> GetPaginatedAsync(Paginator paginator);
    }
}