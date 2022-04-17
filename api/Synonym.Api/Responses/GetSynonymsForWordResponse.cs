using Newtonsoft.Json;

namespace Synonym.Api.Responses;

public record GetSynonymsForWordResponse(string Word, List<string> Synonyms)
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}