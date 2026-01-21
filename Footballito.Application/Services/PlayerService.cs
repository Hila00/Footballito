using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Exceptions;
using Footballito.Application.Interfaces;
using Footballito.Domain.Entities;

namespace Footballito.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly FootballContext _db;

    public PlayerService(FootballContext db)
    {
        _db = db;
    }

    public async Task<List<Player>> GetAllAsync()
    {
        return await _db.Players.AsNoTracking().ToListAsync();
    }

    public async Task<Player> GetByIdAsync(int id)
    {
        var player = await _db.Players.FindAsync(id);
        if (player is null) throw new NotFoundException($"Player {id} not found");
        return player;
    }

    public async Task<Player> CreateAsync(Player player)
    {
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task UpdateAsync(Player player)
    {
        var existing = await _db.Players.FindAsync(player.Id);
        if (existing is null) throw new NotFoundException($"Player {player.Id} not found");
        existing.FirstName = player.FirstName;
        existing.LastName = player.LastName;
        existing.TeamId = player.TeamId;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _db.Players.FindAsync(id);
        if (existing is null) throw new NotFoundException($"Player {id} not found");
        _db.Players.Remove(existing);
        await _db.SaveChangesAsync();
    }
}
