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

    public async Task<ValueResponse> Get(string id)
    {
        try
        {
            var entity = await _valueRepository.Get(id);
            await _unitOfWork.CompleteAsync();
            return new ValueResponse(entity);
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when finding the user: {ex.Message}");
        }
    }

    public async Task<ValueResponse> Save(Value value)
    {
        try
        {
            var entity = await _valueRepository.Save(value);
            await _unitOfWork.CompleteAsync();
            return new ValueResponse(entity);
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when saving the user: {ex.Message}");
        }
    }

    public async Task<ValueResponse> Update(Value value)
    {
        try
        {
            var entity = await _valueRepository.Update(value);
            await _unitOfWork.CompleteAsync();
            return new ValueResponse(entity);
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when updating the user: {ex.Message}");
        }
    }

    public async Task<ValueResponse> Delete(string id)
    {
        try
        {
            var user = await _valueRepository.Get(id);
            var entity = await _valueRepository.Delete(user);
            await _unitOfWork.CompleteAsync();
            return new ValueResponse(entity);
        }
        catch (CustomException ex)
        {
            return new ValueResponse($"An error occurred when removing the user: {ex.Message}");
        }
    }
}