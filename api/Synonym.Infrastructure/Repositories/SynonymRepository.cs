using Synonym.Core.Models;
using Synonym.Core.Repositories;
using Synonym.Infrastructure.Context;

namespace Synonym.Infrastructure.Repositories;

public class SynonymRepository : ISynonymRepository
{
    private readonly InMemoryDbContext _context;

    public SynonymRepository(InMemoryDbContext context)
    {
        _context = context;
    }
    public async Task<Core.Models.Synonym> AddAsync(Core.Models.Synonym entity, CancellationToken cancellationToken = default)
    {
        _context.AddSynonym(entity);
        return entity;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return 0;
    }

    public async Task<List<Word>> GetSynonymsForWord(Word word)
    {
        var res = _context.GetSynonyms().Where(s => s.Word1Id == word.Id).Select(s => s.Word2).ToList();
        return res;
    }
}