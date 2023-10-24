using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace PetStoreApp.UnitTesting
{
    public class PetServiceTests
    {
        [Fact]
        public async Task GetAvailablePetsAsync_ShouldReturnListOfPets()
        {
            // Arrange
            var petService = new PetService();
            var mockHttpClient = new Mock<IHttpClientFactory>();
            var httpClient = new HttpClient();

            mockHttpClient.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await petService.GetAvailablePetsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Pet>>(result);
        }
    }
}
