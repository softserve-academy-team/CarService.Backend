using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using CarService.Api.Models;
using CarService.Api.Mappers;
using CarService.Tests.Extensions;

namespace CarService.Tests.UnitTests.Mappers
{
    public class AutoRiaCarMapperUnitTests
    {
        private readonly ICarMapper _carMapper;
        private readonly string _testFilesFolderPath;

        public AutoRiaCarMapperUnitTests()
        {
            _carMapper = new AutoRiaCarMapper();
            _testFilesFolderPath = $@"{Directory.GetCurrentDirectory()}\TestFiles\AutoRiaCarMapperTestFiles";
        }

        #region MapToCollectionOfCarsIdsTests
        [Fact]
        public async Task MapToCollectionOfCarsIds_CorrectJsonStringObject_ReturnsNotEmptyCollectionOfObjects()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\autoSearchTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                IEnumerable<int> carsIds = _carMapper.MapToCollectionOfCarsIds(jsonString);

                // Assert
                Assert.NotEmpty(carsIds);
            }
        }

        [Fact]
        public async Task MapToCollectionOfCarsIds_CorrectJsonStringObject_ReturnsCollectionOfCorrectAutoIds()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\autoSearchTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                IEnumerable<int> carsIds = _carMapper.MapToCollectionOfCarsIds(jsonString);

                // Assert
                Assert.All(carsIds, autoId =>
                {
                    Assert.True(autoId > 0);
                });
            }
        }
        #endregion

        #region MapToBaseCarInfoObjectTests
        [Fact]
        public async Task MapToBaseCarInfoObject_CorrectJsonStringObject_ReturnsCorrectBaseCarInfoObject()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\allCarInfoTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                BaseCarInfo baseCarInfo = _carMapper.MapToBaseCarInfoObject(jsonString);

                // Assert
                Assert.IsType<BaseCarInfo>(baseCarInfo);
                Assert.True(baseCarInfo.IsValid());
            }
        }
        #endregion

        #region MapToDetailedCarInfoObjectTests
        [Fact]
        public async Task MapToDetailedCarInfoObject_CorrectJsonStringObject_ReturnsCorrectDetailedCarInfoObject()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\allCarInfoTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                DetailedCarInfo detailedCarInfo = _carMapper.MapToDetailedCarInfoObject(jsonString);

                // Assert
                Assert.IsType<DetailedCarInfo>(detailedCarInfo);
                Assert.True(detailedCarInfo.IsValid());
            }
        }
        #endregion

        #region MapToCollectionOfCarPhotoUrisTests
        [Fact]
        public async Task MapToCollectionOfCarPhotoUris_CorrectJsonStringObject_ReturnsNotEmptyCollectionOfObjects()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\carPhotoUrisTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                IEnumerable<string> carPhotoUris = _carMapper.MapToCollectionOfCarPhotoUris(jsonString);

                // Assert
                Assert.NotEmpty(carPhotoUris);
            }
        }

        [Fact]
        public async Task MapToCollectionOfCarPhotoUris_CorrectJsonStringObject_ReturnsCollectionOfCorrectCarPhotoUris()
        {
            // Arrange
            using (var streamReader = new StreamReader($@"{_testFilesFolderPath}\carPhotoUrisTestFile.json"))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                // Act
                IEnumerable<string> carPhotoUris = _carMapper.MapToCollectionOfCarPhotoUris(jsonString);

                // Assert
                Assert.All(carPhotoUris, photoUri =>
                {
                    Assert.False(string.IsNullOrEmpty(photoUri));
                });
            }
        }
        #endregion
    }
}