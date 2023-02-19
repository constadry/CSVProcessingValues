using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
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

    [HttpPost]
    [Route("upload-file")]
    public async Task<IActionResult> ParseCsvFile([FromForm] IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        using var memoryStream = new MemoryStream(new byte[file.Length]);
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Delimiter = ";"
        };
        
        using var reader = new StreamReader(memoryStream);
        using var csv = new CsvReader(reader, configuration);
        csv.Context.TypeConverterCache.AddConverter<DateTime>(new DateConverter());
        csv.Context.RegisterClassMap<ValueMap>();
        var records = csv.GetRecords<Value>();

        var result = await _valueService.SaveAll(file.FileName, records);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllValues([FromQuery] ValueParameters valueParameters)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
            
        var result = await _valueService.GetAll(valueParameters);

        return Ok(result);
    }
}