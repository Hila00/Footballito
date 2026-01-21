namespace Footballito.Api.Models;

public class UpdatePlayerDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int TeamId { get; set; }
}

