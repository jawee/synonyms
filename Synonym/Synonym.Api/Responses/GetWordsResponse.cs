using Newtonsoft.Json;

namespace Synonym.Api.Responses;

public record GetWordsResponse(List<string> Words)
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

}