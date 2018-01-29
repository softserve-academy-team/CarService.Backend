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

        [Fact]
        public async Task GetListOfRandomCars_When_request_Then_list_of_correct_objects()
        {
            // Arrange
            var url = "/api/cars/base-info/random";

            // Act
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BaseCarInfo>>(stringResponse);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.All<BaseCarInfo>(result, obj => {
                Assert.IsType<BaseCarInfo>(obj);
                Assert.True(obj.IsValid());
            });
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_When_request_with_correct_auto_id_Then_correct_object(int autoId)
        {
            // Arrange
            var url = $"/api/cars/detailed-info/{autoId}";

            // Act 
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DetailedCarInfo>(stringResponse);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<DetailedCarInfo>(result);
            Assert.True(result.IsValid());
        }

        [Theory]
        [InlineData(1905098554)]
        [InlineData(111)]
        [InlineData(-7)]        
        public async Task GetDetailedCarInfo_When_request_with_incorrect_auto_id_Then_bad_request_status_code(int autoId)
        {
            // Arrange
            var url = $"/api/cars/detailed-info/{autoId}";

            // Act 
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetCarsPhotos_When_request_with_correct_auto_id_Then_correct_list_of_urls(int autoId)
        {
            // Arrange
            var url = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse);

            // Assert
            Assert.NotNull(result);
            Assert.All<string>(result, obj => {
                Assert.False(string.IsNullOrWhiteSpace(obj));
            });
        }

        [Theory]
        [InlineData(1905098554)]
        [InlineData(111)]        
        [InlineData(-7)]        
        public async Task GetCarsPhotos_When_request_with_incorrect_auto_id_Then_bad_request_status_code(int autoId)
        {
            // Arrange
            var url = $"/api/cars/detailed-info/{autoId}/photos";

            // Act 
            var response = await _httpClient.GetAsync(url);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}