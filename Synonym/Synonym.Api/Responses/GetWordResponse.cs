using Newtonsoft.Json;

namespace Synonym.Api.Responses;

public record GetWordResponse(string Word)
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

}