using Footballito.Domain.Entities;

namespace Footballito.Application.Interfaces;

public interface IMatchService
{
    Task<List<Match>> GetAllAsync();
    Task<Match> GetByIdAsync(int id);
    Task<Match> CreateAsync(Match match);
    Task UpdateAsync(Match match);
    Task DeleteAsync(int id);
}
