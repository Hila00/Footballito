namespace Footballito.Api.Models;

public class CreateMatchDto
{
    public DateTime Date { get; set; }
    public required int HomeTeamId { get; set; }
    public required int AwayTeamId { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
}

