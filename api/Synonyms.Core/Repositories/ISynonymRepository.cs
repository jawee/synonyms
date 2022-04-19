using Synonyms.Core.Models;

namespace Synonyms.Core.Repositories;

public interface ISynonymRepository : IRepository<Synonym>
{
    Task<List<Word>> GetSynonymsForWord(Word word, CancellationToken cancellationToken = default);
}