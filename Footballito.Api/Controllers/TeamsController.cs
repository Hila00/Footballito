using Footballito.Application.Interfaces;
using Footballito.Api.Mappings;
using Footballito.Api.Models;
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
        public async Task<ActionResult<List<TeamDto>>> GetAll()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams.Select(t => t.ToDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamDto>> Get(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            return Ok(team.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> Create([FromBody] CreateTeamDto create)
        {
            if (create is null) return BadRequest();
            var id = _random.Next();
            var team = create.ToEntity(id);
            var createdTeam = await _teamService.CreateAsync(team);
            return CreatedAtAction(nameof(Get), new { id = createdTeam.Id }, createdTeam.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTeamDto update)
        {
            if (update is null) return BadRequest();
            var team = update.ToEntity(id);
            await _teamService.UpdateAsync(team);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
    }
}
