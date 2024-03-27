using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTests.Mocks;
using System.Linq;
using Xunit;

namespace BethanysPieShopTests.Controllers
{
    public class HomeControllerTests
    {

        [Fact]
        public void Index_Use_PieOfTheWeeks_FromRepository()
        {
            //arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();

            HomeController homeController = new HomeController(mockPieRepository.Object);

            //act
            var result = homeController.Index().ViewData.Model
                    as HomeViewModel;

            Assert.NotNull(result);

            //assert
            var piesOfTheWeek = result?.PiesOfTheWeek?.ToList();
            Assert.NotNull(piesOfTheWeek);
            Assert.True(piesOfTheWeek?.Count() == 3);
            Assert.Equal("Apple Pie", piesOfTheWeek?[0].Name);


        }
    }
}
