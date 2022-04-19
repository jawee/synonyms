using Microsoft.Extensions.Logging;
using Synonyms.Core.Models;
using Synonyms.Core.Repositories;

namespace Synonyms.Core.Services;

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
        _logger.LogInformation("WordService.GetWordByString called with input '{word}'", word);
        word = word.ToLower();
        var w = await _repository.GetWordByString(word);

        _logger.LogInformation("WordService.GetWordByString returning '{w}'", w);
        return w;
    }

    public async Task<Word> CreateWord(string word)
    {
        _logger.LogInformation("WordService.CreateWord called with input '{word}'", word);
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

        _logger.LogInformation("WordService.CreateWord returning '{w}'", w);
        return w;
    }
}