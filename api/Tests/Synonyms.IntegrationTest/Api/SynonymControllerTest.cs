using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Synonyms.Api;
using Synonyms.Api.Requests;
using Synonyms.Api.Responses;
using Synonyms.IntegrationTest.Api.Utils;

namespace Synonyms.IntegrationTest.Api;

[TestFixture]
public class SynonymControllerTest
{
    [Test]
    public async Task GET_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var response = await client.GetAsync("/Synonym/a");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>( await response.Content.ReadAsStringAsync());
        
        Assert.That(response.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {response.StatusCode}");
        Assert.That(synonymResponse.Word.Equals("a"), $"Expected to get 'a', got ${synonymResponse.Word}");
        Assert.That(synonymResponse.Synonyms.Contains("b"), $"Expected synonyms to contain 'b'.");
    }
    
    
    [Test]
    public async Task GET_NoQuery_MethodNotAllowed()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var response = await client.GetAsync("/Synonym/");
        
        Assert.That(response.StatusCode == HttpStatusCode.MethodNotAllowed, $"Expected to get MethodNotAllowed, got {response.StatusCode}");
    }
    
    [Test]
    public async Task GET_WordDoesNotExist_EmptyResponse()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var response = await client.GetAsync("/Synonym/f");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>( await response.Content.ReadAsStringAsync());
        
        Assert.That(response.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {response.StatusCode}");
        Assert.That(synonymResponse.Synonyms.Count == 0, $"Expected to get 0 synonyms, got {synonymResponse.Synonyms.Count}");
    }

    [Test]
    public async Task GET_WordDoesNotExist_BadRequest()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var response = await client.GetAsync("/Synonym/f b");
        
        Assert.That(response.StatusCode == HttpStatusCode.BadRequest, $"Expected to get BadRequest, got {response.StatusCode}");
    }
    [Test]
    public async Task POST_WordsDoesNotExist_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest("q","w");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = await client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {resp.StatusCode}");
    }

    [Test]
    public async Task POST_WordDoesExist_Ok()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest("a", "q");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var resp = await client.PostAsync("/Synonym", content);

        var getResp = await client.GetAsync("/Synonym/a");
        var synonymResponse = JsonConvert.DeserializeObject<GetSynonymsForWordResponse>(await getResp.Content.ReadAsStringAsync());
        
        Assert.That(resp.StatusCode == HttpStatusCode.OK, $"Expected to get Ok, got {resp.StatusCode}");
        Assert.That(synonymResponse.Word.Equals("a"), $"Expected to get 'a', got {synonymResponse.Word}");
        Assert.That(synonymResponse.Synonyms.Contains("q"), $"Expected synonyms to contain 'q'");
    }

    [Test]
    public async Task POST_NotSingleWord_BadRequest()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest("a b", "c");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var resp = await client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.BadRequest, $"Expected to get BadRequest, got {resp.StatusCode}");
    }
    
    [Test]
    public async Task POST_NullInput_BadRequest()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest(null, null);
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var resp = await client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.BadRequest, $"Expected to get BadRequest, got {resp.StatusCode}");
    }
    
    [Test]
    public async Task POST_EmptyInput_BadRequest()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest("", "");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var resp = await client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.BadRequest, $"Expected to get BadRequest, got {resp.StatusCode}");
    }
    
    [Test]
    public async Task POST_WhitespaceInput_BadRequest()
    {
        await using var application = new TestWebApplicationFactory<WebMarker>();
        var client = application.CreateClient();
        var request = new CreateSynonymRequest(" ", " ");
        var content = new StringContent(JsonConvert.SerializeObject(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var resp = await client.PostAsync("/Synonym", content);
        
        Assert.That(resp.StatusCode == HttpStatusCode.BadRequest, $"Expected to get BadRequest, got {resp.StatusCode}");
    }
}