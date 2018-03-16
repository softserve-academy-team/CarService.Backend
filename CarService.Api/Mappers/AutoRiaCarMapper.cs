using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using CarService.Api.Models;

namespace CarService.Api.Mappers
{
    public class AutoRiaCarMapper : ICarMapper
    {
        public IEnumerable<int> MapToCollectionOfCarsIds(string jsonString)
        {
            JObject jObject = JObject.Parse(jsonString);
            JToken idsJToken = jObject.SelectToken("result.search_result.ids");
            IEnumerable<int> carsIds = idsJToken.Values<int>();
            return carsIds;
        }
        public BaseCarInfo MapToBaseCarInfoObject(string jsonString)
        {
            JObject jObject = JObject.Parse(jsonString);
            var baseCarInfo = new BaseCarInfo
            {
                AutoId = jObject.SelectToken("autoData.autoId").Value<int>(),
                MarkName = jObject.SelectToken("markName").Value<string>(),
                ModelName = jObject.SelectToken("modelName").Value<string>(),
                Year = jObject.SelectToken("autoData.year").Value<int>(),
                PhotoLink = jObject.SelectToken("photoData.seoLinkSX").Value<string>(),
                PriceUSD = jObject.SelectToken("USD").Value<int>(),
                PriceUAH = jObject.SelectToken("UAH").Value<int>(),
                PriceEUR = jObject.SelectToken("EUR").Value<int>(),
                Race = jObject.SelectToken("autoData.race").Value<string>(),
                RaceInt = jObject.SelectToken("autoData.raceInt").Value<int>(),
                City = jObject.SelectToken("stateData.name").Value<string>(),
                FuelName = jObject.SelectToken("autoData.fuelName").Value<string>(),
                GearBoxName = jObject.SelectToken("autoData.gearboxName").Value<string>()
            };
            return baseCarInfo;
        }
        public DetailedCarInfo MapToDetailedCarInfoObject(string jsonString)
        {
            JObject jObject = JObject.Parse(jsonString);
            var detailedCarInfo = new DetailedCarInfo
            {
                AutoId = jObject.SelectToken("autoData.autoId").Value<int>(),
                MarkName = jObject.SelectToken("markName").Value<string>(),
                ModelName = jObject.SelectToken("modelName").Value<string>(),
                Year = jObject.SelectToken("autoData.year").Value<int>(),
                PhotoLink = jObject.SelectToken("photoData.seoLinkSX").Value<string>(),
                PriceUSD = jObject.SelectToken("USD").Value<int>(),
                PriceUAH = jObject.SelectToken("UAH").Value<int>(),
                PriceEUR = jObject.SelectToken("EUR").Value<int>(),
                Race = jObject.SelectToken("autoData.race").Value<string>(),
                RaceInt = jObject.SelectToken("autoData.raceInt").Value<int>(),
                City = jObject.SelectToken("stateData.name").Value<string>(),
                FuelName = jObject.SelectToken("autoData.fuelName").Value<string>(),
                GearBoxName = jObject.SelectToken("autoData.gearboxName").Value<string>(),
                Description = jObject.SelectToken("autoData.description").Value<string>(),
                CategoryId = jObject.SelectToken("autoData.categoryId").Value<int>(),
                MarkId = jObject.SelectToken("markId").Value<int>(),
                ModelId = jObject.SelectToken("modelId").Value<int>()
            };
            return detailedCarInfo;
        }
        public IEnumerable<string> MapToCollectionOfCarPhotoUris(string jsonString)
        {
            JObject jObject = JObject.Parse(jsonString);
            JEnumerable<JToken> photoJTokens = jObject.SelectToken("data").First.First.Children();
            IEnumerable<string> carPhotoUris = photoJTokens.Select(p => p.First.SelectToken("formats").First.Value<string>());
            return carPhotoUris;
        }
    }
}
