using Footballito.Application.Interfaces;
using Footballito.Domain.Entities;
using Footballito.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Footballito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {

        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamService _teamService;
        private readonly Random _random;

        public TeamsController(ILogger<TeamsController> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
            _random = new Random();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAll() => Ok(await _teamService.GetAllAsync());

        [HttpGet("{id:int}")]
        public ActionResult<Team> Get(int id)
        {
            var team = _teamService.GetByIdAsync(id);
            return team is null ? NotFound("Team is not found") : Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<Team>> Create([FromBody] Team create)
        {
            if (create is null) return BadRequest();
            create.Id = _random.Next();
            await _teamService.CreateAsync(create);
            return CreatedAtAction(nameof(Get), new { id = create.Id }, create);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Team update)
        {
            var existing = await _teamService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            existing.Name = update.Name;
            existing.City = update.City;
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _teamService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
    }
}
