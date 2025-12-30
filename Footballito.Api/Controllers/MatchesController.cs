using Footballito.Application.Interfaces;
using Footballito.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Footballito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly IMatchService _matchService;
        private readonly Random _random;

        public MatchesController(ILogger<MatchesController> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
            _random = new Random();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetAll() => Ok(await _matchService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Match>> Get(int id)
        {
            var match = await _matchService.GetByIdAsync(id);
            return match is null ? NotFound("Match not found") : Ok(match);
        }

        [HttpPost]
        public async Task<ActionResult<Match>> Create([FromBody] Match create)
        {
            if (create is null) return BadRequest();
            create.Id = _random.Next(1, int.MaxValue);
            await _matchService.CreateAsync(create);
            return CreatedAtAction(nameof(Get), new { id = create.Id }, create);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Match update)
        {
            var existing = await _matchService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            existing.Date = update.Date;
            existing.HomeTeamId = update.HomeTeamId;
            existing.AwayTeamId = update.AwayTeamId;
            existing.HomeTeamScore = update.HomeTeamScore;
            existing.AwayTeamScore = update.AwayTeamScore;
            await _matchService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _matchService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            await _matchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
