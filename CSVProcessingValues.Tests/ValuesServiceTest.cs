using Moq;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories;
using CSVProcessingValues.Services;
using CSVProcessingValues.Services.ValueService;
using Xunit;

namespace CSVProcessingValues.Tests;

public class ValuesServiceTest
{
    private readonly IValueService _valueService;
    private readonly ValueParameters _valueParams;
    private readonly Mock<IValueRepository> _mockRepository;

    public ValuesServiceTest()
    {
        _mockRepository = new Mock<IValueRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        _valueService = new ValueService(_mockRepository.Object, mockUnitOfWork.Object);
        _valueParams = new ValueParameters();
    }

    [Fact]
    public void TestAllTime()
    {
        
    }
}