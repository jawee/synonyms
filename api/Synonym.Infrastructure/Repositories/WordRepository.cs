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
        await Task.Run(() => _context.AddWord(entity), cancellationToken);
        return entity;
    }

    public async Task<Word?> GetWordByString(string s, CancellationToken cancellationToken = default)
    {
        var res = await Task.Run(() => _context.GetWords().FirstOrDefault(w => w.Value.Equals(s)), cancellationToken);
        return res;
    }
}