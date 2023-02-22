using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CSVProcessingValues.Extensions;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories.ResultRepository;
using CSVProcessingValues.Services.ResultService;
using Moq;
using Xunit;

namespace CSVProcessingValues.Tests;

public class ValuesServiceTest
{
    [Theory, AutoMoqData]
    public void TestResults([Frozen] Mock<IResultRepository> repository, List<Value> values)
    {
        var resultService = new ResultService(repository.Object);
        var expectedResult = new Result
        {
            FileName = "test",
            Period = GetAllTime(values),
            StartDate = GetStartDateTime(values),
            AverageTime = GetAverageTime(values),
            AverageIndication = GetAverageIndicator(values),
            MedianIndication = GetMedianIndicator(values),
            MinIndication = values.MinBy(x => x.Indication).Indication,
            MaxIndication = values.MaxBy(x => x.Indication).Indication,
            ValuesCount = values.Count
        };

        var actualResult = resultService.GetResult(values, "test");
        
        //Id doesn't matter for this test
        expectedResult.Id = actualResult.Id;
        
        Assert.Equivalent(expectedResult, actualResult);
    }

    private int GetAllTime(List<Value> values)
    {
        var maxValue = values.MaxBy(x => x.Time)?.Time ?? 0;
        var minValue = values.MinBy(x => x.Time)?.Time ?? 0;

        return maxValue - minValue;
    }
    
    public DateTime GetStartDateTime(List<Value> values)
    {
        return values.MinBy(x => x.StartDate).StartDate;
    }
    
    public double GetAverageTime(List<Value> values)
    {
        return values.Average(x => x.Time);
    }
    
    public double GetAverageIndicator(List<Value> values)
    {
        return values.Average(x => x.Indication);
    }
    
    public double GetMedianIndicator(List<Value> values)
    {
        return values.Median(x => x.Indication).Value;
    }
}

public class AutoMoqDataAttribute : AutoDataAttribute
{
    [Obsolete("Obsolete")]
    public AutoMoqDataAttribute()
        : base(new Fixture()
            .Customize(new AutoMoqCustomization()))
    {
        
    }
}