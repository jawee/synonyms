using Synonyms.Infrastructure.Context;

namespace Synonyms.Test.Utils;

public static class TestSynonymDbContext
{
    public static InMemoryDbContext GetTestDbContext()
    {
        return new InMemoryDbContext();
    }
}