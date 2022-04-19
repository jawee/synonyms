using Synonyms.Core.Models;

namespace Synonyms.Core.Services;

public interface IWordService
{
    Task<Word?> GetWordByString(string word);

    Task<Word> CreateWord(string word);

}