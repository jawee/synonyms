using Microsoft.Extensions.Logging;
using Synonym.Core.Models;
using Synonym.Core.Repositories;

namespace Synonym.Core.Services;

public class SynonymService : ISynonymService
{

    private readonly ISynonymRepository _repository;
    private readonly IWordService _wordService;
    private readonly ILogger<SynonymService> _logger;

    public SynonymService(ISynonymRepository repository, IWordService wordService, ILogger<SynonymService> logger)
    {
        _repository = repository;
        _wordService = wordService;
        _logger = logger;
    }
    public async Task CreateSynonym(string firstWord, string secondWord)
    {
        _logger.LogInformation("SynonymService.CreateSynonym called with input '{firstWord}' '{secondWord'}", firstWord, secondWord);
        firstWord = firstWord.ToLower();
        secondWord = secondWord.ToLower();
        
        var first = await _wordService.CreateWord(firstWord);
        
        var firstSynonyms = await _repository.GetSynonymsForWord(first);
        
        if (SynonymAlreadyExists(secondWord, firstSynonyms))
        {
            _logger.LogDebug("SynonymService.CreateSynonym Synonym already exists for '{firstWord}' -> '{secondWord}'. Returning.", firstWord, secondWord);
            return;
        }
        
        var second = await _wordService.CreateWord(secondWord);
        
        var secondSynonyms = await _repository.GetSynonymsForWord(second);
        
        await CreateSynonyms(first, second);

        await HandleTransitiveRule(secondWord, firstSynonyms);

        await HandleTransitiveRule(firstWord, secondSynonyms);
        
        _logger.LogInformation("SynonymService.CreateSynonym Synonyms has been created for '{firstWord}' -> '{secondWord}'.", firstWord, secondWord);
    }

    private async Task CreateSynonyms(Word first, Word second)
    {
        _logger.LogDebug("SynonymService.CreateSynonyms called for '{first}' -> '{second}'", first, second);
        var synonym = new Core.Models.Synonym
        {
            Word1 = first,
            Word2 = second
        };

        await _repository.AddAsync(synonym);

        synonym = new Core.Models.Synonym
        {
            Word1 = second,
            Word2 = first
        };

        await _repository.AddAsync(synonym);
        _logger.LogDebug("SynonymService.CreateSynonyms done for '{first}' -> '{second}'", first, second);
    }

    public async Task<List<string>> GetSynonymsForWord(string wordString)
    {
        _logger.LogInformation("SynonymService.GetSynonymsForWord called with input '{wordString}'", wordString);
        wordString = wordString.ToLower();
        
        var word = await _wordService.GetWordByString(wordString);
        if (word is null)
        {
            return new List<string>();
        }
        var words = await _repository.GetSynonymsForWord(word);
        var res = words.Select(w => w.Value).ToList();
        
        _logger.LogInformation("SynonymService.GetSynonymsForWord returning '{res}'", res);
        return res;
    }
    
    private async Task HandleTransitiveRule(string wordString, List<Word> synonyms)
    {
        _logger.LogDebug("SynonymService.HandleTransitiveRule called with input '{wordString}' '{synonyms}'.", wordString, synonyms);
        foreach (var word in synonyms)
        {
            await CreateSynonym(word.Value, wordString);
        }
        _logger.LogDebug("SynonymService.HandleTransitiveRule done for '{wordString}' '{synonyms}'.", wordString, synonyms);
    }

    private bool SynonymAlreadyExists(string word, List<Word> synonyms)
    {
        _logger.LogDebug("SynonymService.SynonymAlreadyExists called with input '{word}' '{synonyms}'", word, synonyms);
        return synonyms.FirstOrDefault(w => w.Value.Equals(word)) != null;
    }

}