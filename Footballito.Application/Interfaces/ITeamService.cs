using Footballito.Persistence;

namespace Footballito.Application.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);
    Task<Team> CreateAsync(Team team);
    Task<bool> UpdateAsync(Team team);
    Task<bool> DeleteAsync(int id);
}
