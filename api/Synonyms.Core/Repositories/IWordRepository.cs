using Synonyms.Core.Models;

namespace Synonyms.Core.Repositories;

public interface IWordRepository : IRepository<Word>
{
   Task<Word?> GetWordByString(string s, CancellationToken cancellationToken = default);
}