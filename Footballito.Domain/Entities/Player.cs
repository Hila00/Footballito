namespace Footballito.Domain.Entities;

public class Player
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int TeamId { get; set; }
}