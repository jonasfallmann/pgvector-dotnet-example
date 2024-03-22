using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace PGVectorDemo.Database;

public class DemoDbContext(DbContextOptions<DemoDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public  DbSet<DemoEntity> DemoEntities => Set<DemoEntity>();
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(DemoDbContext).Assembly);
        builder.HasPostgresExtension("vector");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        optionsBuilder.UseNpgsql(connectionString, o => o.UseVector());
    }
}