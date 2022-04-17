using Synonym.Core.Models;

namespace Synonym.Core.Services;

public interface IWordService
{
    Task<Word?> GetWordByString(string word);

    Task<Word> CreateWord(string word);

}