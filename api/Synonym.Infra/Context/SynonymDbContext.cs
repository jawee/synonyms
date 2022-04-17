using Microsoft.EntityFrameworkCore;
using Synonym.Core.Models;

namespace Synonym.Infra.Context;

public class SynonymDbContext : DbContext
{
    public SynonymDbContext(DbContextOptions<SynonymDbContext> options) : base(options)
    {
    }
    
    public DbSet<Word> Words => Set<Word>();
    public DbSet<Core.Models.Synonym> Synonyms => Set<Core.Models.Synonym>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(word => {
            word.HasIndex(w => w.Value).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}