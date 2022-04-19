using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Synonym.Api.Requests;

public record CreateSynonymRequest(string FirstWord, string SecondWord)
{
    
    private const string RegexPattern = @"^[a-zA-ZÅÄÖåäö]*$";
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public bool Validate()
    {
        return !ValidateWord(FirstWord) || !ValidateWord(SecondWord);
    }
    
    private bool ValidateWord(string word)
    {
        return !string.IsNullOrEmpty(word) && Regex.IsMatch(word, RegexPattern);
    }

};