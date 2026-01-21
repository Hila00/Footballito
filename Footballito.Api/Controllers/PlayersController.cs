using Footballito.Application.Interfaces;
using Footballito.Api.Mappings;
using Footballito.Api.Models;
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
        public async Task<ActionResult<List<PlayerDto>>> GetAll()
        {
            var players = await _playerService.GetAllAsync();
            return Ok(players.Select(p => p.ToDto()).ToList());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerDto>> Get(int id)
        {
            var player = await _playerService.GetByIdAsync(id);
            return Ok(player.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> Create([FromBody] CreatePlayerDto create)
        {
            if (create is null) return BadRequest();
            var id = _random.Next(1, int.MaxValue);
            var player = create.ToEntity(id);
            var createdPlayer = await _playerService.CreateAsync(player);
            return CreatedAtAction(nameof(Get), new { id = createdPlayer.Id }, createdPlayer.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePlayerDto update)
        {
            if (update is null) return BadRequest();
            var player = update.ToEntity(id);
            await _playerService.UpdateAsync(player);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _playerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
