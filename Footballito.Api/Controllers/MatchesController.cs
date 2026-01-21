using Footballito.Application.Interfaces;
using Footballito.Api.Mappings;
using Footballito.Api.Models;
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
        public async Task<ActionResult<List<MatchDto>>> GetAll()
        {
            var matches = await _matchService.GetAllAsync();
            return Ok(matches.Select(m => m.ToDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MatchDto>> Get(int id)
        {
            var match = await _matchService.GetByIdAsync(id);
            return Ok(match.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<MatchDto>> Create([FromBody] CreateMatchDto create)
        {
            if (create is null) return BadRequest();
            var id = _random.Next(1, int.MaxValue);
            var match = create.ToEntity(id);
            var createdMatch = await _matchService.CreateAsync(match);
            return CreatedAtAction(nameof(Get), new { id = createdMatch.Id }, createdMatch.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateMatchDto update)
        {
            if (update is null) return BadRequest();
            var match = update.ToEntity(id);
            await _matchService.UpdateAsync(match);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _matchService.DeleteAsync(id);
            return NoContent();
        }
    }
}
