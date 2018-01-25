using Newtonsoft.Json.Linq;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public class AutoRiaCarMapper : ICarMapper
    {
        public BaseCarInfo MapToBaseCarInfoObject(string jsonString)
        {
            var jObject = JObject.Parse(jsonString);
            return new BaseCarInfo
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
                City = jObject.SelectToken("stateData.name").Value<string>()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            };
        }
    }
}