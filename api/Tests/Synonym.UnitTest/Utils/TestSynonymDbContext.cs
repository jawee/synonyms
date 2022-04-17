using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Synonym.Infra.Context;

namespace Synonym.Test.Utils;

public class TestSynonymDbContext
{
    public static SynonymDbContext GetTestDbContext()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<SynonymDbContext>().UseSqlite(connection).Options;

        var context = new SynonymDbContext(options);
            
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        return context;
    }
}