using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using MissionSystem.Data.Converters;
using MissionSystem.Data.Models;

namespace MissionSystem.Data;

public class DataStore : DbContext
{
    public DbSet<Gadget> Gadgets { get; set; }
    public DbSet<Arena> Arenas { get; set; }
    public DbSet<GadgetType> GadgetTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=postgres; Username=postgres; Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gadget>().Navigation(g => g.Type).AutoInclude();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<PhysicalAddress>()
            .HaveConversion<PhysicalAddressConverter>();
    }
}
