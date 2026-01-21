using Footballito.Domain.Entities;

namespace Footballito.Application.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetAllAsync();
    Task<Team> GetByIdAsync(int id);
    Task<Team> CreateAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(int id);
}
