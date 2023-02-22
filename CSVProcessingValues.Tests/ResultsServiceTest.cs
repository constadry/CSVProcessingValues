using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Models;
using CSVProcessingValues.Services.ValidationService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace CSVProcessingValues.Tests;

public class ResultsServiceTest
{
    [Theory, AutoMoqData]
    public async Task TestResults(
        [Frozen] Mock<IValidationService> validationService,
        List<Result> results,
        // Mock<AppDbContext> context,
        ResultParameters parameters)
    {
        var myDbContextMock = new Mock<AppDbContext>();
        myDbContextMock.Setup(x => x.Results).ReturnsDbSet(results);
        
        var expectedResults = FilterResults(myDbContextMock.Object.Results, parameters);

        var actualResults = await validationService.
            Object.
            FilterResultsAsync(myDbContextMock.Object.Results, parameters);
        
        Assert.Equivalent(expectedResults, actualResults);
    }

    private List<Result> FilterResults(DbSet<Result> results, ResultParameters parameters)
    {
        return results.Where(
            x => (x.StartDate >= parameters.StartDate || x.StartDate <= parameters.EndDate)
                 && (x.AverageTime >= parameters.AverageTimeMin || x.AverageTime <= parameters.AverageTimeMax)
                 && (x.AverageIndication >= parameters.AverageIndicationMin || x.AverageIndication <= parameters.AverageIndicationMax)
        ).ToList();
    }
}