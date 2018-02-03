using System.Collections.Generic;
using CarService.Api.Models;

namespace CarService.Api.Mappers
{
    public interface ICarMapper
    {
        IEnumerable<int> MapToCollectionOfCarsIds(string jsonString);
        BaseCarInfo MapToBaseCarInfoObject(string jsonString);
        DetailedCarInfo MapToDetailedCarInfoObject(string jsonString);
        IEnumerable<string> MapToCollectionOfCarPhotoUris(string jsonString);
    }
}