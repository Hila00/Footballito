using Footballito.Api.Models;
using Footballito.Domain.Entities;

namespace Footballito.Api.Mappings;

public static class EntityMappings
{
    // Player mappings
    public static PlayerDto ToDto(this Player player)
    {
        return new PlayerDto
        {
            Id = player.Id,
            FirstName = player.FirstName,
            LastName = player.LastName,
            TeamId = player.TeamId
        };
    }

    public static Player ToEntity(this CreatePlayerDto dto, int id)
    {
        return new Player
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TeamId = dto.TeamId
        };
    }

    public static Player ToEntity(this UpdatePlayerDto dto, int id)
    {
        return new Player
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TeamId = dto.TeamId
        };
    }

    // Team mappings
    public static TeamDto ToDto(this Team team)
    {
        return new TeamDto
        {
            Id = team.Id,
            Name = team.Name,
            City = team.City
        };
    }

    public static Team ToEntity(this CreateTeamDto dto, int id)
    {
        return new Team
        {
            Id = id,
            Name = dto.Name,
            City = dto.City
        };
    }

    public static Team ToEntity(this UpdateTeamDto dto, int id)
    {
        return new Team
        {
            Id = id,
            Name = dto.Name,
            City = dto.City
        };
    }

    // Match mappings
    public static MatchDto ToDto(this Match match)
    {
        return new MatchDto
        {
            Id = match.Id,
            Date = match.Date,
            HomeTeamId = match.HomeTeamId,
            AwayTeamId = match.AwayTeamId,
            HomeTeamScore = match.HomeTeamScore,
            AwayTeamScore = match.AwayTeamScore
        };
    }

    public static Match ToEntity(this CreateMatchDto dto, int id)
    {
        return new Match
        {
            Id = id,
            Date = dto.Date,
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            HomeTeamScore = dto.HomeTeamScore,
            AwayTeamScore = dto.AwayTeamScore
        };
    }

    public static Match ToEntity(this UpdateMatchDto dto, int id)
    {
        return new Match
        {
            Id = id,
            Date = dto.Date,
            HomeTeamId = dto.HomeTeamId,
            AwayTeamId = dto.AwayTeamId,
            HomeTeamScore = dto.HomeTeamScore,
            AwayTeamScore = dto.AwayTeamScore
        };
    }
}

