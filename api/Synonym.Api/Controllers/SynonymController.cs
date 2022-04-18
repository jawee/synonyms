using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Synonym.Api.Requests;
using Synonym.Api.Responses;
using Synonym.Core.Services;

namespace Synonym.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SynonymController : ControllerBase
{
    private readonly ISynonymService _service;
    private readonly ILogger<SynonymController> _logger;
    
    private const string RegexPattern = @"^[a-zA-ZÅÄÖåäö]*$";

    public SynonymController(ISynonymService service, ILogger<SynonymController> logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("{word}")]
    public async Task<ActionResult<GetSynonymsForWordResponse>> GetSynonymsForWord(string word)
    { 
        _logger.LogInformation("GetSynonymsForWord called with input '{word}'", word);
        var res = await _service.GetSynonymsForWord(word);

        if (!ValidateWord(word))
        {
            return BadRequest("Word should be a single word.");
        }
        var response = new GetSynonymsForWordResponse(word, res);
    
        _logger.LogInformation("Returning response '{response}'", response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateSynonymRequest request)
    {
        _logger.LogInformation("CreateSynonym called with input '{request}'", request);
        if (request.Validate())
        {
            return BadRequest("FirstWord and SecondWord should be single words.");
        }
        
        await _service.CreateSynonym(request.FirstWord, request.SecondWord);

        _logger.LogInformation("CreateSynonym returning ok");
        return Ok();
    }

    private bool ValidateWord(string word)
    {
        return Regex.IsMatch(word, RegexPattern);
    }
}