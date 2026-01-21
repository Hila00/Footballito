using System.Collections.Generic;

namespace Footballito.Domain.Entities;

public class Team
{
    public required int Id { get; set; }
    public required string? Name { get; set; }
    public required string? City { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
    public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
    public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
}
