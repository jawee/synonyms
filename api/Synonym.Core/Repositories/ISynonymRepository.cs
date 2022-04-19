using Synonym.Core.Models;

namespace Synonym.Core.Repositories;

public interface ISynonymRepository : IRepository<Models.Synonym>
{
    Task<List<Word>> GetSynonymsForWord(Word word, CancellationToken cancellationToken = default);
}