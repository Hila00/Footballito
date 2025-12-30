using Footballito.Application.Interfaces;
using Footballito.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Footballito.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IMatchService, MatchService>();
        return services;
    }
}
