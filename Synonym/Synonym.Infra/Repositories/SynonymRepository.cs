using Microsoft.EntityFrameworkCore;
using Synonym.Core.Models;
using Synonym.Core.Repositories;
using Synonym.Infra.Context;

namespace Synonym.Infra.Repositories;

public class SynonymRepository : RepositoryBase<Core.Models.Synonym>, ISynonymRepository
{
    public SynonymRepository(SynonymDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Word>> GetSynonymsForWord(Word word)
    {
        var synonymIds = await DbContext.Set<Core.Models.Synonym>().Where(s => s.Word1Id == word.Id).Select(s => s.Word2Id).ToListAsync();

        var words = await DbContext.Set<Word>().Where(w => synonymIds.Contains(w.Id)).ToListAsync();

        return words;
    }
}