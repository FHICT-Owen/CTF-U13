using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using MissionSystem.Data.Converters;
using MissionSystem.Interface.Models;

namespace MissionSystem.Data;

public class DataStore : DbContext
{
    public DbSet<Gadget> Gadgets { get; set; }
    // public DbSet<Match> Matches { get; set; }
    public DbSet<Arena> Arenas { get; set; }
    public DbSet<GadgetType> GadgetTypes { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=postgres; Username=postgres; Password=postgres;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gadget>().Navigation(g => g.Type).AutoInclude();
        modelBuilder.Entity<Gadget>().HasIndex(g => g.MacAddress).IsUnique();
        // modelBuilder.Entity<GadgetType>().HasIndex(t => t.RefId).IsUnique();

        // modelBuilder.Entity<Arena>().Navigation(a => a.Game).AutoInclude();
        modelBuilder.Entity<Match>().Navigation(m => m.Gadgets).AutoInclude();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<PhysicalAddress>()
            .HaveConversion<PhysicalAddressConverter>();
    }
}
