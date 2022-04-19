using Synonym.Core.Models;
using Synonym.Core.Repositories;
using Synonym.Infrastructure.Context;

namespace Synonym.Infrastructure.Repositories;

public class WordRepository : IWordRepository
{
    private readonly InMemoryDbContext _context;

    public WordRepository(InMemoryDbContext context)
    {
        _context = context;
    }

    public async Task<Word> AddAsync(Word entity, CancellationToken cancellationToken = default)
    {
        _context.AddWord(entity);
        return entity;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return 0;
    }

    public async Task<Word?> GetWordByString(string s)
    {
        var res = _context.GetWords().FirstOrDefault(w => w.Value.Equals(s));
        return res;
    }
}