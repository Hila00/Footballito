using Footballito.Domain.Entities;

namespace Footballito.Application.Interfaces;

public interface IPlayerService
{
    Task<List<Player>> GetAllAsync();
    Task<Player> GetByIdAsync(int id);
    Task<Player> CreateAsync(Player player);
    Task UpdateAsync(Player player);
    Task DeleteAsync(int id);
}
