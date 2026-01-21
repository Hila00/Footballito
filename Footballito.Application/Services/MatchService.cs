using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Exceptions;
using Footballito.Application.Interfaces;
using Footballito.Domain.Entities;

namespace Footballito.Application.Services;

public class MatchService : IMatchService
{
    private readonly FootballContext _db;

    public MatchService(FootballContext db)
    {
        _db = db;
    }

    public async Task<List<Match>> GetAllAsync()
    {
        return await _db.Matches.AsNoTracking().ToListAsync();
    }

    public async Task<Match> GetByIdAsync(int id)
    {
        var match = await _db.Matches.FindAsync(id) ?? throw new NotFoundException($"Match {id} not found");
        return match;
    }

    public async Task<Match> CreateAsync(Match match)
    {
        _db.Matches.Add(match);
        await _db.SaveChangesAsync();
        return match;
    }

    public async Task UpdateAsync(Match match)
    {
        var existing = await _db.Matches.FindAsync(match.Id) ?? throw new NotFoundException($"Match {match.Id} not found");
        existing.Date = match.Date;
        existing.HomeTeamId = match.HomeTeamId;
        existing.AwayTeamId = match.AwayTeamId;
        existing.HomeTeamScore = match.HomeTeamScore;
        existing.AwayTeamScore = match.AwayTeamScore;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await _db.Matches.FindAsync(id) ?? throw new NotFoundException($"Match {id} not found");
        _db.Matches.Remove(existing);
        await _db.SaveChangesAsync();
    }
}
