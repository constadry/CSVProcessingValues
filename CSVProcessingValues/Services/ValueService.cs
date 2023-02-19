using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories;
using CSVProcessingValues.Tools;

namespace CSVProcessingValues.Services;

public class ValueService : IValueService
{
    private readonly IValueRepository _valueRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ValueService(IValueRepository valueRepository, IUnitOfWork unitOfWork)
    {
        _valueRepository = valueRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters)
    {
        return _valueRepository.GetAll(valueParameters);
    }

    public async Task<ValueResponse> SaveAll(string fileName, IEnumerable<Value> values)
    {
        try
        {
            values = values.Where(x =>
                x is { Indication: >= 0, Time: >= 0 } &&
                x.StartDate <= DateTime.Now &&
                x.StartDate >= DateTime.Parse("01.01.2000"))
                .Take(10000).ToList();
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