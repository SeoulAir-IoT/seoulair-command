using System;
using System.Threading.Tasks;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Repositories;
using SeoulAir.Command.Domain.Interfaces.Services;

namespace SeoulAir.Command.Domain.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;

        public CommandService(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<CommandDto> FindAsync(string id)
        {
            return await _commandRepository.FindAsync(new Guid(id));
        }

        public async Task<CommandDto> FindByNameAsync(string name)
        {
            return await _commandRepository.FindByNameAsync(name);
        }

        public async Task<CommandDto> AddAsync(CommandDto course)
        {
            return await _commandRepository.AddAsync(course);
        }

        public async Task DeleteAsync(string id)
        {
            await _commandRepository.DeleteAsync(new Guid(id));
        }

        public Task<PaginatedResultDto<CommandDto>> GetPaginated(Paginator paginator)
        {
            return _commandRepository.GetPaginatedAsync(paginator);
        }

        public void Update(string id, CommandDto dto)
        {
            _commandRepository.UpdateAsync(new Guid(id), dto);
        }
    }
}
