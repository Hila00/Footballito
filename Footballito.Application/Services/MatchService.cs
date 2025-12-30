using Microsoft.EntityFrameworkCore;
using Footballito.Persistence;
using Footballito.Application.Interfaces;

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

    public async Task<Match?> GetByIdAsync(int id)
    {
        return await _db.Matches.FindAsync(id);
    }

    public async Task<Match> CreateAsync(Match match)
    {
        _db.Matches.Add(match);
        await _db.SaveChangesAsync();
        return match;
    }

    public async Task<bool> UpdateAsync(Match match)
    {
        var existing = await _db.Matches.FindAsync(match.Id);
        if (existing is null) return false;
        existing.Date = match.Date;
        existing.HomeTeamId = match.HomeTeamId;
        existing.AwayTeamId = match.AwayTeamId;
        existing.HomeTeamScore = match.HomeTeamScore;
        existing.AwayTeamScore = match.AwayTeamScore;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _db.Matches.FindAsync(id);
        if (existing is null) return false;
        _db.Matches.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
