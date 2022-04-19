namespace Synonyms.Core.Services;

public interface ISynonymService
{
    Task CreateSynonym(string firstWord, string secondWord);
    Task<List<string>> GetSynonymsForWord(string word);
}