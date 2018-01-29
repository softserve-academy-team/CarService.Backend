using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using CarService.Api.Models;

namespace CarService.Api.Services
{
    public class AutoRiaCarService : ICarService
    {
        private readonly IConfiguration _configuration;
        private readonly ICarUrlBuilder _carUrlBuilder;
        private readonly ICarMapper _carMapper;
        private readonly HttpClient _httpClient;

        public AutoRiaCarService(IConfiguration configuration, ICarUrlBuilder carUrlBuilder, ICarMapper carMapper)
        {
            _configuration = configuration;
            _carUrlBuilder = carUrlBuilder;
            _carMapper = carMapper;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<int>> GetListOfCarsIds(IDictionary<string, string> carParameters)
        {
            carParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);
            string url = _carUrlBuilder.Build(_configuration["AutoRiaApi:AutoSearchUrl"], carParameters);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            return jObject.SelectToken("result.search_result.ids").Values<int>();
        }
        public async Task<IEnumerable<int>> GetListOfRandomCarsIds()
        {
            var carParameters = new Dictionary<string, string>();
            carParameters.Add("сategory_id", "1");
            return await GetListOfCarsIds(carParameters);
        }
        public async Task<IEnumerable<BaseCarInfo>> GetBaseInfoAboutCars(IEnumerable<int> autoIds)
        {
            var res = new List<BaseCarInfo>();
            foreach (var id in autoIds)
            {
                var allInfo = await GetAllCarInfo(id);
                res.Add(_carMapper.MapToBaseCarInfoObject(allInfo));
            }
            return res;
        }
        public async Task<DetailedCarInfo> GetDetailedCarInfo(int autoId)
        {
            var detailedInfo = await GetAllCarInfo(autoId);
            return _carMapper.MapToDetailedCarInfoObject(detailedInfo);
        }
        public async Task<IEnumerable<string>> GetCarsPhotos(int autoId)
        {
            var carParameters = new Dictionary<string, string>();
            carParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);
            string url = _carUrlBuilder.Build($"{_configuration["AutoRiaApi:AutoPhotosUrl"]}/{autoId}", carParameters);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            var jsonPhotos = jObject.SelectToken($"data.{autoId}").Children();
            var res = new List<string>();
            foreach (var jsonPhoto in jsonPhotos)
                res.Add(jsonPhoto.First.SelectToken("formats").First.Value<string>());
            return res;
        }

        private async Task<string> GetAllCarInfo(int autoId)
        {
            var carParameters = new Dictionary<string, string>();
            carParameters.Add("auto_id", autoId.ToString());
            carParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);
            string url = _carUrlBuilder.Build(_configuration["AutoRiaApi:AutoInfoUrl"], carParameters);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}