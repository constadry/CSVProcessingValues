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
    public void TestAllTime([Frozen] Mock<IResultRepository> repository, List<Value> values)
    {
        var resultService = new ResultService(values, repository.Object);
        int expectedNumber = GetAllTime(values);
        
        var res = resultService.GetAllTime();
        
        Assert.Equal(expectedNumber, res);
    }

    [Theory, AutoMoqData]
    public void TestStartTime([Frozen] Mock<IResultRepository> repository, List<Value> values)
    {
        var resultService = new ResultService(values, repository.Object);
        var expectedDate = GetStartDateTime(values);
        
        var res = resultService.GetStartDateTime();
        
        Assert.Equal(expectedDate, res);
    }
    
    [Theory, AutoMoqData]
    public void TestAveragesAndMedian([Frozen] Mock<IResultRepository> repository, List<Value> values)
    {
        var resultService = new ResultService(values, repository.Object);
        var expectedAverageTime = GetAverageTime(values);
        var expectedAverageIndicator = GetAverageIndicator(values);
        var expectedMedian = GetMedianIndicator(values);
        
        var time = resultService.GetAverageTime();
        var indicator = resultService.GetAverageIndicator();
        var median = resultService.GetMedianIndicator();
        
        Assert.Equal(expectedAverageTime, time);
        Assert.Equal(expectedAverageIndicator, indicator);
        Assert.Equal(expectedMedian, median);
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