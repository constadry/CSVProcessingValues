using CSVProcessingValues.Models;
using CSVProcessingValues.Services;
using Microsoft.AspNetCore.Mvc;
using CSVProcessingValues.Extensions;

namespace CSVProcessingValues.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IValueService _valueService;
    public ValuesController(IValueService valueService)
    {
        _valueService = valueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllValues([FromQuery] ValueParameters valueParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
            
        var result = await _valueService.GetAll(valueParameters);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetValue(string id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var result = await _valueService.Get(id);

        if (!result.Success) return BadRequest(result.Message);

        return Ok(result.Value);
    }
}