using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Interfaces;
using Footballito.Domain.Entities;

namespace Footballito.Application.Services;

public class TeamService : ITeamService
{
    private readonly FootballContext _db;

    public TeamService(FootballContext db)
    {
        _db = db;
    }

    public async Task<List<Team>> GetAllAsync()
    {
        return await _db.Teams.AsNoTracking().ToListAsync();
    }

    public async Task<Team?> GetByIdAsync(int id)
    {
        return await _db.Teams.FindAsync(id);
    }

    public async Task<Team> CreateAsync(Team team)
    {
        _db.Teams.Add(team);
        await _db.SaveChangesAsync();
        return team;
    }

    public async Task<bool> UpdateAsync(Team team)
    {
        var existing = await _db.Teams.FindAsync(team.Id);
        if (existing is null) return false;
        existing.Name = team.Name;
        existing.City = team.City;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Teams.FindAsync(id);
        if (existing is null) return false;
        _db.Teams.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
