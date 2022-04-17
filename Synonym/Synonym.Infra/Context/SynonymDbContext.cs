using Microsoft.EntityFrameworkCore;
using Synonym.Core.Models;

namespace Synonym.Infra.Context;

public class SynonymDbContext : DbContext
{
    // public string DbPath { get; }
    public SynonymDbContext(DbContextOptions<SynonymDbContext> options) : base(options)
    {
    }
    
    // public SynonymDbContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = Path.Join(path, "blogging.db");
    // }
    public DbSet<Word> Words => Set<Word>();
    public DbSet<Core.Models.Synonym> Synonyms => Set<Core.Models.Synonym>();
    
    
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source=test");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(word => {
            word.HasIndex(w => w.Value).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}