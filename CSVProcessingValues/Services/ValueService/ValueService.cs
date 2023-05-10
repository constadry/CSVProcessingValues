using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories;
using CSVProcessingValues.Repositories.ValueRepository;
using CSVProcessingValues.Services.ValidationService;
using CSVProcessingValues.Tools;

namespace CSVProcessingValues.Services.ValueService;

public class ValueService : IValueService
{
    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService _validationService;
    

    public ValueService(IValueRepository valueRepository, IUnitOfWork unitOfWork, IValidationService validationService)
    {
        _valueRepository = valueRepository;
        _unitOfWork = unitOfWork;
        _validationService = validationService;
    }

    public async Task<ValueResponse> GetAll(string fileName)
    {
        try
        {
            var results = await _valueRepository.GetAll(fileName);
            return new ValueResponse(results);
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when saving the user: {ex.Message}");
        }
        
    }

    public async Task<ValueResponse> SaveAll(string fileName, List<Value> values)
    {
        try
        {
            _validationService.CheckValidValues(values);
            foreach (var value in values) value.FileName = fileName;
            await _valueRepository.SaveAll(values);
            await _unitOfWork.CompleteAsync();
            return new ValueResponse(values.ToList());
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when saving the user: {ex.Message}");
        }
    }
}