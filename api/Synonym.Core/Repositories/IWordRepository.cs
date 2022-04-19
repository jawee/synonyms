using Synonym.Core.Models;

namespace Synonym.Core.Repositories;

public interface IWordRepository : IRepository<Word>
{
   Task<Word?> GetWordByString(string s, CancellationToken cancellationToken = default);
}