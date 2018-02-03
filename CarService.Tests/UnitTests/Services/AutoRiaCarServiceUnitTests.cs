using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CarService.Api.Models;
using CarService.Api.Mappers;
using CarService.Api.Services;
using CarService.Tests.Extensions;

namespace CarService.Tests.UnitTests.Services
{
    public class AutoRiaCarServiceUnitTests
    {
        private readonly IConfiguration _configuration;
        private readonly ICarMapper _carMapper;
        private readonly ICarService _carService;
        private readonly string _testFilesFolderPath;


        public AutoRiaCarServiceUnitTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = configurationBuilder.Build();
            _carMapper = new AutoRiaCarMapper();
            _carService = new AutoRiaCarService(_configuration, _carMapper);
            _testFilesFolderPath = string.Format("{0}{1}TestFiles{1}AutoRiaCarServiceTestFiles{1}", Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar);
        }

        #region GetCarsIdsTests
        [Fact]
        public async Task GetCarsIds_CorrectCarsParameters_ReturnsNotEmptyCollectionOfIds()
        {
            // Arrange
            using (var streamReader = new StreamReader($"{_testFilesFolderPath}carsParametersTestFile.json"))
            {
                string carsParametersJsonString = await streamReader.ReadToEndAsync();
                IDictionary<string, string> carsParameters = JsonConvert.DeserializeObject<IDictionary<string, string>>(carsParametersJsonString);

                // Act
                IEnumerable<int> carsIds = await _carService.GetCarsIds(carsParameters);

                // Assert
                Assert.NotEmpty(carsIds);
            }
        }

        [Fact]
        public async Task GetCarsIds_CorrectCarsParameters_ReturnsCollectionOfCorrectIds()
        {
            // Arrange
            using (var streamReader = new StreamReader($"{_testFilesFolderPath}carsParametersTestFile.json"))
            {
                string carsParametersJsonString = await streamReader.ReadToEndAsync();
                IDictionary<string, string> carsParameters = JsonConvert.DeserializeObject<IDictionary<string, string>>(carsParametersJsonString);

                // Act
                IEnumerable<int> carsIds = await _carService.GetCarsIds(carsParameters);

                // Assert
                Assert.All<int>(carsIds, autoId => Assert.True(autoId > 0));
            }
        }

        [Fact]
        public async Task GetCarsIds_IncorrectCarsParameters_ReturnsRandomCollectionOfCarsIds()
        {
            // Arrange
            var carsParameters = new Dictionary<string, string>
            {
                {"abc", "12"},
                {"dac", "gdf"},
                {"", ""},
                {"ecc", "5443"}
            };

            // Act
            IEnumerable<int> carsIds = await _carService.GetCarsIds(carsParameters);

            // Assert
            Assert.All<int>(carsIds, autoId => Assert.True(autoId > 0));
        }

        [Fact]
        public async Task GetCarsIds_EmptyDictionaryOfCarsParameters_ReturnsRandomCollectionOfCarsIds()
        {
            // Arrange
            var carsParameters = new Dictionary<string, string>();

            // Act
            IEnumerable<int> carsIds = await _carService.GetCarsIds(carsParameters);

            // Assert
            Assert.All<int>(carsIds, autoId => Assert.True(autoId > 0));
        }
        #endregion

        #region GetRandomCarsIdsTests
        [Fact]
        public async Task GetRandomCarsIds_ReturnsNotEmptyCollectionOfIds()
        {
            // Act
            IEnumerable<int> carsIds = await _carService.GetRandomCarsIds();

            // Assert
            Assert.NotEmpty(carsIds);
        }

        [Fact]
        public async Task GetRandomCarsIds_ReturnsCollectionOfCorrectIds()
        {
            // Act
            IEnumerable<int> carsIds = await _carService.GetRandomCarsIds();

            // Assert
            Assert.All<int>(carsIds, id => Assert.True(id > 0));
        }
        #endregion

        #region GetBaseInfoAboutCarsTests
        [Fact]
        public async Task GetBaseInfoAboutCars_CorrectCarsIds_ReturnsNotEmptyCollectionOfObjects()
        {
            // Arrange
            using (var streamReader = new StreamReader($"{_testFilesFolderPath}carsIdsTestFile.json"))
            {
                string carsIdsJsonString = await streamReader.ReadToEndAsync();
                JObject jObject = JObject.Parse(carsIdsJsonString);
                IEnumerable<int> carsIds = jObject.SelectToken("carsIds").Values<int>();

                // Act
                IEnumerable<BaseCarInfo> info = await _carService.GetBaseInfoAboutCars(carsIds);

                // Assert
                Assert.NotEmpty(info);
            }
        }

        [Fact]
        public async Task GetBaseInfoAboutCars_CorrectCarsIds_ReturnsCollectionOfCorrectBaseCarInfoObjects()
        {
            // Arrange
            using (var streamReader = new StreamReader($"{_testFilesFolderPath}carsIdsTestFile.json"))
            {
                string carsIdsJsonString = await streamReader.ReadToEndAsync();
                JObject jObject = JObject.Parse(carsIdsJsonString);
                IEnumerable<int> carsIds = jObject.SelectToken("carsIds").Values<int>();

                // Act
                IEnumerable<BaseCarInfo> info = await _carService.GetBaseInfoAboutCars(carsIds);

                // Assert
                Assert.All(info, baseCarInfo =>
                {
                    Assert.IsType<BaseCarInfo>(baseCarInfo);
                    Assert.True(baseCarInfo.IsValid());
                });
            }
        }

        [Fact]
        public async Task GetCarsIds_EmptyCollectionOfCarsIds_ReturnsEmptyCollectionOfBaseCarInfoObjects()
        {
            // Arrange
            var carsIds = new List<int>();

            // Act
            IEnumerable<BaseCarInfo> info = await _carService.GetBaseInfoAboutCars(carsIds);

            // Assert
            Assert.Empty(info);
        }
        #endregion

        #region GetDetailedCarInfoTests
        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_CorrectAutoId_ReturnsDetailedCarInfoObject(int autoId)
        {
            // Act 
            DetailedCarInfo detailedCarInfo = await _carService.GetDetailedCarInfo(autoId);

            // Assert
            Assert.NotNull(detailedCarInfo);
            Assert.IsType<DetailedCarInfo>(detailedCarInfo);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetDetailedCarInfo_CorrectAutoId_ReturnsCorrectDetailedCarInfoObject(int autoId)
        {
            // Act 
            DetailedCarInfo detailedCarInfo = await _carService.GetDetailedCarInfo(autoId);

            // Assert
            Assert.True(detailedCarInfo.IsValid());
        }
        #endregion

        #region GetCarPhotosTests
        [Theory]
        [InlineData(19050985)]
        public async Task GetCarPhotos_CorrectAutoId_ReturnsNotEmptyCollectionOfObjects(int autoId)
        {
            // Act
            IEnumerable<string> carPhotos = await _carService.GetCarPhotos(autoId);

            //Assert
            Assert.NotEmpty(carPhotos);
        }

        [Theory]
        [InlineData(19050985)]
        public async Task GetCarPhotos_CorrectAutoId_ReturnsCollectionOfCorrectPhotosUris(int autoId)
        {
            // Act
            IEnumerable<string> carPhotos = await _carService.GetCarPhotos(autoId);

            //Assert
            Assert.All<string>(carPhotos, carPhoto =>
            {
                Assert.False(string.IsNullOrWhiteSpace(carPhoto));
            });
        }
        #endregion
    }
}