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
        return !ValidateWord(this.FirstWord) || !ValidateWord(this.SecondWord);
    }
    
    private bool ValidateWord(string word)
    {
        return Regex.IsMatch(word, RegexPattern);
    }

};