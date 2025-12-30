using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Interfaces;

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

    public async Task<Player?> GetByIdAsync(int id)
    {
        return await _db.Players.FindAsync(id);
    }

    public async Task<Player> CreateAsync(Player player)
    {
        _db.Players.Add(player);
        await _db.SaveChangesAsync();
        return player;
    }

    public async Task<bool> UpdateAsync(Player player)
    {
        var existing = await _db.Players.FindAsync(player.Id);
        if (existing is null) return false;
        existing.FirstName = player.FirstName;
        existing.LastName = player.LastName;
        existing.TeamId = player.TeamId;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Players.FindAsync(id);
        if (existing is null) return false;
        _db.Players.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
