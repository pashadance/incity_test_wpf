using incity_test.Model;
using Microsoft.EntityFrameworkCore;

namespace incity_test.DataAccess;

public class IncityDbContext : DbContext
{
    public DbSet<DocumentDb> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Incity_test;Pooling=true"; 
        optionsBuilder.UseNpgsql(connectionString, builder => builder.MigrationsHistoryTable("__EFMigrationsHistory", "test"))
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DocumentDb>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Guid).IsRequired();
            entity.Property(e => e.RegistrationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.ActualAddress).HasMaxLength(500);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });
    }
}