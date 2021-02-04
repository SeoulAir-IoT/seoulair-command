using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Repositories;
using SeoulAir.Command.Repositories.Extensions;

namespace SeoulAir.Command.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly IMapper _mapper;
        private readonly SeoulAirCommandDbContext _dbContext;
        private readonly DbSet<Entities.Command> _commands;

        public CommandRepository(IMapper mapper, SeoulAirCommandDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _commands = _dbContext.Commands;
        }
        
        public async Task<CommandDto> FindAsync(Guid id)
        {
            return _mapper.Map<CommandDto>(await _commands.FindAsync(id));
        }

        public async Task<CommandDto> FindByNameAsync(string name)
        {
            var result = _commands.Where(entity => entity.Name == name).AsNoTracking();
            return _mapper.Map<CommandDto>(await result.FirstOrDefaultAsync());
        }

        public async Task<CommandDto> AddAsync(CommandDto course)
        {
            Entities.Command entity = _mapper.Map<Entities.Command>(course);
            entity.Id = Guid.NewGuid();

            await _commands.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            
            return _mapper.Map<CommandDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            Entities.Command courseToDelete = await _commands.FirstOrDefaultAsync(entity => entity.Id == id);
            
            _commands.Remove(courseToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedResultDto<CommandDto>> GetPaginatedAsync(Paginator paginator)
        {
            return await _commands.AsNoTracking().GetPaginatedAsync<CommandDto, Entities.Command>(paginator, _mapper);
        }

        public async Task UpdateAsync(Guid id, CommandDto dto)
        {
            var updatedCourse = _mapper.Map<Entities.Command>(dto);
            updatedCourse.Id = id;            

            _dbContext.Entry(updatedCourse).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
