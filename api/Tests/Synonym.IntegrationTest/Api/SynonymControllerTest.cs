using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Synonym.Api;
using Synonym.Api.Requests;
using Synonym.Api.Responses;
using Synonym.IntegrationTest.Api.Utils;

namespace Synonym.IntegrationTest.Api;

[TestFixture]
public class SynonymControllerTest
{
    [Test]
    public async Task GET_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var _client = application.CreateClient();
        var response = await _client.GetAsync("/Synonym/a");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>( await response.Content.ReadAsStringAsync());
        
        Assert.That(response.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {response.StatusCode}");
        Assert.That(synonymResponse.Word.Equals("a"), $"Expected to get 'a', got ${synonymResponse.Word}");
        Assert.That(synonymResponse.Synonyms.Contains("b"), $"Expected synonyms to contain 'b'.");
    }
    
    [Test]
    public async Task GET_WordDoesNotExist_EmptyResponse()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var _client = application.CreateClient();
        var response = await _client.GetAsync("/Synonym/f");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>( await response.Content.ReadAsStringAsync());
        
        Assert.That(response.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {response.StatusCode}");
        Assert.That(synonymResponse.Synonyms.Count == 0, $"Expected to get 0 synonyms, got {synonymResponse.Synonyms.Count}");
    }

    [Test]
    public async Task POST_WordsDoesNotExist_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var _client = application.CreateClient();
        var request = new CreateSynonymRequest("q","w");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = await _client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {resp.StatusCode}");
    }

    [Test]
    public async Task POST_WordDoesExist_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var _client = application.CreateClient();
        var request = new CreateSynonymRequest("a", "q");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = await _client.PostAsync("/Synonym", content);

        var getResp = await _client.GetAsync("/Synonym/a");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>(await getResp.Content.ReadAsStringAsync());
        
        Assert.That(resp.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {resp.StatusCode}");
        Assert.That(synonymResponse.Word.Equals("a"), $"Expected to get 'a', got {synonymResponse.Word}");
        Assert.That(synonymResponse.Synonyms.Contains("q"), $"Expected synonyms to contain 'q'");
    }
}