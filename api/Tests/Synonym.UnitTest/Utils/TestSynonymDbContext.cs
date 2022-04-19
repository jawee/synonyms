using Synonym.Infrastructure.Context;

namespace Synonym.Test.Utils;

public static class TestSynonymDbContext
{
    public static InMemoryDbContext GetTestDbContext()
    {
        return new InMemoryDbContext();
    }
}