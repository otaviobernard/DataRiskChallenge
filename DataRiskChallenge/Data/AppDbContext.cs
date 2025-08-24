using DataRiskChallenge.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DataRiskChallenge.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Script> Scripts => Set<Script>();
    public DbSet<Execution> Executions => Set<Execution>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Script>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Identifier)
                .IsRequired();
        });
        
        modelBuilder.Entity<Execution>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Identifier)
                .IsRequired();
        });
    }
}