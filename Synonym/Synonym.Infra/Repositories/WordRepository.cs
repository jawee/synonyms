using Microsoft.EntityFrameworkCore;
using Synonym.Core.Models;
using Synonym.Core.Repositories;
using Synonym.Infra.Context;

namespace Synonym.Infra.Repositories;

public class WordRepository : RepositoryBase<Word>, IWordRepository
{
    public WordRepository(SynonymDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Word?> GetWordByString(string s)
    {
        var result = await DbContext.Set<Word>().FirstOrDefaultAsync(w => w.Value == s);
        
        return result;
    }
}