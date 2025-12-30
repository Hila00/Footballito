using Footballito.Application.Interfaces;
using Footballito.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Footballito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayerService _playerService;
        private readonly Random _random;

        public PlayersController(ILogger<PlayersController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
            _random = new Random();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetAll() => Ok(await _playerService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            return player is null ? NotFound("Player not found") : Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Create([FromBody] Player create)
        {
            if (create is null) return BadRequest();
            create.Id = _random.Next(1, int.MaxValue);
            await _playerService.CreateAsync(create);
            return CreatedAtAction(nameof(Get), new { id = create.Id }, create);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Player update)
        {
            var existing = await _playerService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            existing.FirstName = update.FirstName;
            existing.LastName = update.LastName;
            existing.TeamId = update.TeamId;
            await _playerService.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _playerService.GetByIdAsync(id);
            if (existing is null) return NotFound();
            await _playerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
