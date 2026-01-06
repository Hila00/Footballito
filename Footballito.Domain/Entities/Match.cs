namespace Footballito.Domain.Entities;

public class Match
{
    public required int Id { get; set; }
    public DateTime Date { get; set; }
    public required int HomeTeamId { get; set; }
    public required int AwayTeamId { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
}

