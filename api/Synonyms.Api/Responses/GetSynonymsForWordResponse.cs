using Newtonsoft.Json;

namespace Synonyms.Api.Responses;

public record GetSynonymsForWordResponse(string Word, List<string> Synonyms)
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}