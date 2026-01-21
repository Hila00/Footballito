using Footballito.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Footballito.Persistence;

public class FootballContext : DbContext
{
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }

    public string DbPath { get; }

    public FootballContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "footballito.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.HomeTeam)
            .WithMany(t => t.HomeMatches)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>().HasData(
            new Team { Id = 1, Name = "FC Barcelona", City = "Barcelona" },
            new Team { Id = 2, Name = "Real Madrid", City = "Madrid" },
            new Team { Id = 3, Name = "Manchester United", City = "Manchester" },
            new Team { Id = 4, Name = "Liverpool FC", City = "Liverpool" },
            new Team { Id = 5, Name = "Bayern Munich", City = "Munich" },
            new Team { Id = 6, Name = "Juventus", City = "Turin" },
            new Team { Id = 7, Name = "Paris Saint-Germain", City = "Paris" },
            new Team { Id = 8, Name = "Chelsea FC", City = "London" },
            new Team { Id = 9, Name = "AC Milan", City = "Milan" },
            new Team { Id = 10, Name = "River Plate", City = "Buenos Aires" }
        );
        modelBuilder.Entity<Player>().HasData(
            new Player { Id = 1, FirstName = "Lionel", LastName = "Messi", TeamId = 1 },
            new Player { Id = 2, FirstName = "Sergio", LastName = "Busquets", TeamId = 1 },
            new Player { Id = 3, FirstName = "Cristiano", LastName = "Ronaldo", TeamId = 2 },
            new Player { Id = 4, FirstName = "Karim", LastName = "Benzema", TeamId = 2 },
            new Player { Id = 5, FirstName = "Marcus", LastName = "Rashford", TeamId = 3 },
            new Player { Id = 6, FirstName = "Mohamed", LastName = "Salah", TeamId = 4 }
        );
        modelBuilder.Entity<Match>().HasData(
            new Match { Id = 1, Date = new DateTime(2025, 1, 15), HomeTeamId = 1, AwayTeamId = 2, HomeTeamScore = 2, AwayTeamScore = 1 },
            new Match { Id = 2, Date = new DateTime(2025, 2, 20), HomeTeamId = 3, AwayTeamId = 4, HomeTeamScore = 1, AwayTeamScore = 3 },
            new Match { Id = 3, Date = new DateTime(2025, 3, 10), HomeTeamId = 5, AwayTeamId = 6, HomeTeamScore = 0, AwayTeamScore = 0 }
        );
    }
}



