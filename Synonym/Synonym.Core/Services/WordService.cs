using Microsoft.Extensions.Logging;
using Synonym.Core.Models;
using Synonym.Core.Repositories;

namespace Synonym.Core.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _repository;
    private readonly ILogger<WordService> _logger;

    public WordService(IWordRepository repository, ILogger<WordService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Word?> GetWordByString(string word)
    {
        word = word.ToLower();
        var w = await _repository.GetWordByString(word);

        return w;
    }

    public async Task<Word> CreateWord(string word)
    {
        word = word.ToLower();
        
        var exists = await _repository.GetWordByString(word);
        if (exists != null)
        {
            return exists;
        }
        var w = new Word
        {
            Value = word
        };

        await _repository.AddAsync(w);

        return w;
    }
}