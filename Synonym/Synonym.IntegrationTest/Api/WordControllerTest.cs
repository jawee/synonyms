using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Synonym.Api;
using Synonym.Api.Responses;
using Synonym.Test.Api.Utils;

namespace Synonym.Test.Api;

[TestFixture]
public class WordControllerTest
{

    [Test]
    public async Task GET_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        using var client = application.CreateClient();
        var response = await client.GetAsync("/Words/a");
        var word = JsonConvert.DeserializeObject<GetWordResponse>( await response.Content.ReadAsStringAsync());
        
        Assert.That(response.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {response.StatusCode}");
        Assert.That(word.Word.Equals("a"), $"Expected to get 'a', got ${word.Word}");
    }
    
    [Test]
    public async Task GET_NotFound()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/Words/e");
        Assert.That(response.StatusCode == HttpStatusCode.NotFound, $"Expected to get NotFound, got {response.StatusCode}");
    }
}