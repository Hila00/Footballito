namespace Footballito.Domain.Entities;

public class Team
{
    public required int Id { get; set; }
    public required string? Name { get; set; }
    public required string? City { get; set; }
}
