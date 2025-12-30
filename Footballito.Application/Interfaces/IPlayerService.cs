using Footballito.Persistence;

namespace Footballito.Application.Interfaces;

public interface IPlayerService
{
    Task<List<Player>> GetAllAsync();
    Task<Player?> GetByIdAsync(int id);
    Task<Player> CreateAsync(Player player);
    Task<bool> UpdateAsync(Player player);
    Task<bool> DeleteAsync(int id);
}
