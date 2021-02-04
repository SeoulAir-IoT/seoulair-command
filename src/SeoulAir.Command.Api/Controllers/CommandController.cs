using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Domain.Interfaces.Services;

namespace SeoulAir.Command.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _coursesService;

        public CommandController(ICommandService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandDto>> FindAsync(string id)
        {
            return Ok(await _coursesService.FindAsync(id));
        }
        
        [HttpGet]
        public async Task<ActionResult<CommandDto>> FindByNameAsync([FromQuery] string name)
        {
            return Ok(await _coursesService.FindByNameAsync(name));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CommandDto commandDto)
        {
            return Ok(await _coursesService.AddAsync(commandDto));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _coursesService.DeleteAsync(id);
            
            return NoContent();
        }

        [HttpGet("page")]
        public async Task<ActionResult<PaginatedResultDto<CommandDto>>> GetPaginatedAsync(
            [FromQuery] Paginator paginator)
        {
            var result = await _coursesService.GetPaginated(paginator);
            
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(string id, CommandDto dto)
        {
            _coursesService.Update(id, dto);

            return Ok();
        }
    }
}