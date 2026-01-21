using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Exceptions;
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

    public async Task<Team> GetByIdAsync(int id)
    {
        var team = await _db.Teams.FindAsync(id) ?? throw new NotFoundException($"Team {id} not found");
        return team;
    }

    public async Task<Team> CreateAsync(Team team)
    {
        _db.Teams.Add(team);
        await _db.SaveChangesAsync();
        return team;
    }

    public async Task UpdateAsync(Team team)
    {
        var existing = await _db.Teams.FindAsync(team.Id);
        if (existing is null) throw new NotFoundException($"Team {team.Id} not found");
        existing.Name = team.Name;
        existing.City = team.City;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _db.Teams.FindAsync(id) ?? throw new NotFoundException($"Team {id} not found");
        var hasPlayers = await _db.Players.AnyAsync(p => p.TeamId == id);
        var hasMatches = await _db.Matches.AnyAsync(m => m.HomeTeamId == id || m.AwayTeamId == id);
        if (hasPlayers || hasMatches)
        {
            throw new ConflictException($"Team {id} cannot be deleted because it has related players or matches.");
        }

        _db.Teams.Remove(existing);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ConflictException($"Unable to delete team {id}: {ex.Message}");
        }
    }
}
