using Microsoft.AspNetCore.Mvc;
using Synonym.Api.Responses;
using Synonym.Core.Services;

namespace Synonym.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WordsController : ControllerBase
{

   private readonly IWordService _service;

   public WordsController(IWordService service)
   {
      _service = service;
   }
   
   [HttpGet("{word}")]
   public async Task<ActionResult<GetWordResponse>> GetWord(string word)
   {
      var res = await _service.GetWordByString(word);
      if (res is null)
      {
         return NotFound();
      }

      var response = new GetWordResponse(res.Value);
      
      return Ok(response);
   }
}