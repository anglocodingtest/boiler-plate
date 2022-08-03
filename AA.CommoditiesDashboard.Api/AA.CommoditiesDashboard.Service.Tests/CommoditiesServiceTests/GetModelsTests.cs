using AA.CommoditiesDashboard.Service.Dtos;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AA.CommoditiesDashboard.Service.Tests.CommoditiesServiceTests;

[TestClass]
public class GetModelsTests : CommoditiesServiceTestsBase
{
    [TestMethod]
    public async Task GivenCallToGetCommodities_ThenReturnCommodities()
    {
        //act
        var result = await Target.GetModels();

        //assert
        result.Should().BeEquivalentTo(GetExpectedModels());
    }

    private IEnumerable<ModelDto> GetExpectedModels()
    {
        return new List<ModelDto>
        {
            new () { Id = 1, Name = "S&P" },
            new () { Id = 2, Name = "FTSE" }
        };
    }
}
