using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using CarService.Api.Models;
using CarService.Tests.Extensions;

namespace CarService.Tests.IntegrationTests.Controllers
{
    public class CarsControllerIntegrationTests : ApiControllerTestBase
    {
        private readonly HttpClient _httpClient;

        public CarsControllerIntegrationTests()
        {
            _httpClient = base.GetClient();
        }

        #region GetRandomCarsTests
        [Fact]
        public async Task GetRandomCars_SendsHttpRequest_ReturnsResponseWithSuccessStatusCode()
        {
            // Arrange
            string uri = "/api/cars/base-info/random";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetRandomCars_SendsHttpRequest_ReturnsResponseWithNotEmptyStringContent()
        {
            // Arrange
            string uri = "/api/cars/base-info/random";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.False(string.IsNullOrEmpty(stringResponse));
        }

        [Fact]
        public async Task GetRandomCars_SendsHttpRequest_ReturnsResponseWithNotEmptyCollectionOfObjects()
        {
            // Arrange
            string uri = "/api/cars/base-info/random";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<BaseCarInfo> randomCars = JsonConvert.DeserializeObject<IEnumerable<BaseCarInfo>>(stringResponse);

            // Assert
            Assert.NotNull(randomCars);
            Assert.NotEmpty(randomCars);
        }

        [Fact]
        public async Task GetRandomCars_SendsHttpRequest_ReturnsResponseWithCollectionOfCorrectBaseCarInfoObjects()
        {
            // Arrange
            string uri = "/api/cars/base-info/random";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<BaseCarInfo> randomCars = JsonConvert.DeserializeObject<IEnumerable<BaseCarInfo>>(stringResponse);

            // Assert
            Assert.All<BaseCarInfo>(randomCars, car =>
            {
                Assert.IsType<BaseCarInfo>(car);
                Assert.True(car.IsValid());
            });
        }
        #endregion

        #region GetDetailedCarInfoTests
        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithSuccessStatusCode(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithNotEmptyStringContent(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.False(string.IsNullOrEmpty(stringResponse));
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithCorrectDetailedCarInfoObject(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            DetailedCarInfo detailedCarInfo = JsonConvert.DeserializeObject<DetailedCarInfo>(stringResponse);

            // Assert
            Assert.NotNull(detailedCarInfo);
            Assert.IsType<DetailedCarInfo>(detailedCarInfo);
            Assert.True(detailedCarInfo.IsValid());
        }

        [Theory]
        [InlineData(1905098554)]
        [InlineData(111)]
        [InlineData(-7)]
        public async Task GetDetailedCarInfo_SendsHttpRequestWithIncorrectAutoId_ReturnsResponseWithNotFoundStatusCode(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region GetCarsPhotosTests
        [Theory]
        [InlineData(19050985)]
        public async Task GetCarsPhotos_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithSuccessStatusCode(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetCarsPhotos_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithNotEmptyStringContent(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.False(string.IsNullOrEmpty(stringResponse));
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetCarsPhotos_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithNotEmptyCollectionOfObjects(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string> carsPhotos = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse);

            // Assert
            Assert.NotNull(carsPhotos);
            Assert.NotEmpty(carsPhotos);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetCarsPhotos_SendsHttpRequestWithCorrectAutoId_ReturnsResponseWithCollectionOfCorrectPhotoUris(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<string> carsPhotos = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse);

            // Assert
            Assert.All<string>(carsPhotos, photoUri =>
            {
                Assert.False(string.IsNullOrWhiteSpace(photoUri));
            });
        }

        [Theory]
        [InlineData(1905098554)]
        [InlineData(111)]
        [InlineData(-7)]
        public async Task GetCarsPhotos_SendsHttpRequestWithIncorrectAutoId_ReturnsResponseWithNotFoundStatusCode(int autoId)
        {
            // Arrange
            string uri = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion
    }
}