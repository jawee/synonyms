using Synonyms.Core.Models;
using Synonyms.Core.Repositories;
using Synonyms.Infrastructure.Context;

namespace Synonyms.Infrastructure.Repositories;

public class SynonymRepository : ISynonymRepository
{
    private readonly InMemoryDbContext _context;

    public SynonymRepository(InMemoryDbContext context)
    {
        _context = context;
    }
    public async Task<Synonym> AddAsync(Synonym entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => _context.AddSynonym(entity), cancellationToken);
        return entity;
    }

    public async Task<List<Word>> GetSynonymsForWord(Word word, CancellationToken cancellationToken = default)
    {
        var res = await Task.Run(() => _context.GetSynonyms().Where(s => s.Word1Id == word.Id).Select(s => s.Word2).ToList(), cancellationToken);
        return res;
    }
}