using Newtonsoft.Json;

namespace Synonym.Api.Requests;

public record CreateSynonymRequest(string FirstWord, string SecondWord)
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
};