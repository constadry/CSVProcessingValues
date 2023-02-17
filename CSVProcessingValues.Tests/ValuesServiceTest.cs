using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Moq;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories;
using CSVProcessingValues.Services;
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
    public async Task GetAllUsers()
    {
        _mockRepository.Setup(repo=> repo.GetAll(_valueParams)).Returns(GetTestUsers);

        var result = await _valueService.GetAll(_valueParams);

        Assert.Equal(GetTestUsers().Result.Count(), result.Count());
    }

    [Fact]
    public async Task GetUser()
    {
        const string userId = "dfssasd2";
        _mockRepository.Setup(repo=> repo.Get(userId)).Returns(GetTestUser);

        var result = await _valueService.Get(userId);

        // Assert.Equal(GetTestUser().Result.Name, result.User.Name);
    }

    private static async Task<Value> GetTestUser()
    {
        return new Value
        {
            // UserId = "dfssasd2",
            // Name = "Dave",
            // Age = 35,
            // Sex = Sex.Male
        };
    }

    private static async Task<IEnumerable<Value>> GetTestUsers()
    {
        var users = new List<Value>
        {
            // new() { UserId = "sasvsd1", Name = "Tom", Age = 35, Sex = Sex.Male},
            // new() { UserId = "sasvfd1", Name = "Alice", Age = 29, Sex = Sex.Female},
            // new() { UserId = "sgsvsd1", Name = "Sam", Age = 32, Sex = Sex.Male},
            // new() { UserId = "sasvbd1", Name = "Kate", Age = 30, Sex = Sex.Female}
        };
        return users;
    }
}