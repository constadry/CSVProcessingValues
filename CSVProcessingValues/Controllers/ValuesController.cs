using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CSVProcessingValues.Models;
using Microsoft.AspNetCore.Mvc;
using CSVProcessingValues.Extensions;
using CSVProcessingValues.Services.ResultService;
using CSVProcessingValues.Services.ValueService;

namespace CSVProcessingValues.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IValueService _valueService;
    private readonly IResultService _resultService;
    public ValuesController(IValueService valueService, IResultService resultService)
    {
        _valueService = valueService;
        _resultService = resultService;
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
        var records = csv.GetRecords<Value>()?.ToList() ?? new List<Value>();

        var resultValues = await _valueService.SaveAll(file.FileName, records);
        await _resultService.ExecuteAsync(records.ToList(), file.FileName);
        
        if (resultValues.Success)
            return Ok(resultValues.Values);
        return BadRequest(resultValues.Message);
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