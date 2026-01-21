namespace Footballito.Api.Models;

public class CreatePlayerDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int TeamId { get; set; }
}

