using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MissionSystem.Data.Converters;
using MissionSystem.Interface.Models;

namespace MissionSystem.Data;

public class DataStore : DbContext
{
    public virtual DbSet<Gadget> Gadgets { get; set; }
    public virtual DbSet<Arena> Arenas { get; set; }
    public virtual DbSet<GadgetType> GadgetTypes { get; set; }

    private IConfiguration? _cfg;

    public DataStore(IConfiguration cfg)
    {
        _cfg = cfg;
    }

    public DataStore()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_cfg == null)
        {
            return;
        }

        var connectionString = _cfg.GetConnectionString("DefaultConnection");

        optionsBuilder
            .UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly("MissionSystem.Data"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gadget>().Navigation(g => g.Type).AutoInclude();
        modelBuilder.Entity<GadgetType>().HasIndex(t => t.RefId).IsUnique();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<PhysicalAddress>()
            .HaveConversion<PhysicalAddressConverter>();
    }
}
