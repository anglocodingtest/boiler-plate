using AA.CommoditiesDashboard.Api.Controllers;
using AA.CommoditiesDashboard.Api.Domain;
using AA.CommoditiesDashboard.Api.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AA.CommoditiesDashboard.Api.Tests
{
    public class CommoditiesControllerTests
    {

        [Fact]
        public void Constructor_WhenCommoditiesServiceIsNull_ThrowsArgumentNullException()
        {
            // Arrange/Act
            Action act = () => new CommoditiesController(null);

            // Assert
            act.Should()
                .Throw<Exception>("constructor should fail when passing null for parameter: {0}", "CommoditiesService");
        }

        [Fact]
        public void Constructor_WhenCommoditiesServiceIsNotNull_CreatesController()
        {
            // Arrange
            var service = new Mock<ICommoditiesService>();
            
            //Act
            var sut = new CommoditiesController(service.Object);
            
            // Assert
            sut.Should().NotBeNull();
        }
        
        [Fact]
        public async Task Get_ReturnsExpectedModel()
        {
            // Arrange
            var expected = new ModelCommodity
            {
                Id = 1,
                ModelName = "Model1",
                CommodityName = "Commodity1",
                PnLDaily = 100,
                PnlLTD = 200,
                Position = 1,
                Price = 10,
                VarAllocation = 99
            };

            var service = new Mock<ICommoditiesService>();

            service.Setup(x => x.GetModelCommodities()).ReturnsAsync(new List<ModelCommodity> { expected });

            // Act
            var sut = new CommoditiesController(service.Object);

            // Assert
            var res = await sut.Get();
            var actual = res.First();

            actual.Should().BeEquivalentTo(expected);
        }

        // TODO: Add more tests
    }
}