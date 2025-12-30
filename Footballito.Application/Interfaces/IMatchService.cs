using Footballito.Persistence;

namespace Footballito.Application.Interfaces;

public interface IMatchService
{
    Task<List<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<Match> CreateAsync(Match match);
    Task<bool> UpdateAsync(Match match);
    Task<bool> DeleteAsync(int id);
}
