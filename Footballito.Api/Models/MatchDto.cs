namespace Footballito.Api.Models;

public class MatchDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
}

