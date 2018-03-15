using System;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using CarService.Api.Models;
using CarService.Api.Mappers;

namespace CarService.Api.Services
{
    public class AutoRiaCarService : ICarService
    {
        private readonly IConfiguration _configuration;
        private readonly ICarMapper _carMapper;
        private readonly HttpClient _httpClient;

        public AutoRiaCarService(IConfiguration configuration, ICarMapper carMapper)
        {
            _configuration = configuration;
            _carMapper = carMapper;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<int>> GetCarsIds(IDictionary<string, string> carsParameters)
        {
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);
            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = _configuration["AutoRiaApi:AutoSearchPath"];
            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            IEnumerable<int> carsIds = _carMapper.MapToCollectionOfCarsIds(stringResponse);
            return carsIds;
        }
        public async Task<IEnumerable<int>> GetRandomCarsIds()
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("сategory_id", "1");
            IEnumerable<int> carsIds = await GetCarsIds(carsParameters);
            return carsIds;
        }
        public async Task<IEnumerable<BaseCarInfo>> GetBaseInfoAboutCars(IEnumerable<int> autoIds)
        {
            var cars = new List<BaseCarInfo>();
            foreach (var autoId in autoIds)
            {
                string allCarInfo = await GetAllCarInfo(autoId);
                cars.Add(_carMapper.MapToBaseCarInfoObject(allCarInfo));
            }
            return cars;
        }
        public async Task<DetailedCarInfo> GetDetailedCarInfo(int autoId)
        {
            string allCarInfo = await GetAllCarInfo(autoId);
            DetailedCarInfo detailedCarInfo = _carMapper.MapToDetailedCarInfoObject(allCarInfo);
            return detailedCarInfo;
        }
        public async Task<IEnumerable<string>> GetCarPhotos(int autoId)
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);

            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = $"{_configuration["AutoRiaApi:AutoPhotosPath"]}/{autoId}";
            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            IEnumerable<string> carPhotos = _carMapper.MapToCollectionOfCarPhotoUris(stringResponse);
            return carPhotos;
        }

        private async Task<string> GetAllCarInfo(int autoId)
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("auto_id", autoId.ToString());
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);

            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = _configuration["AutoRiaApi:AutoInfoPath"];
            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            HttpResponseMessage response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            string allCarInfo = await response.Content.ReadAsStringAsync();
            return allCarInfo;
        }

        public async Task<string> GetInitialTypesDropdownInfo()
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);

            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = $"{_configuration["AutoRiaApi:AutoTypesPath"]}";
            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            var response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetMakesDropdownInfo(int categoryId)
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);

            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = $"{_configuration["AutoRiaApi:AutoTypesPath"]}/{categoryId}/marks";

            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            var response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetModelsDropdownInfo(int categoryId, int makeId)
        {
            var carsParameters = new Dictionary<string, string>();
            carsParameters.Add("api_key", _configuration["AutoRiaApi:ApiKey"]);

            var uriBuilder = new UriBuilder(_configuration["AutoRiaApi:Scheme"], _configuration["AutoRiaApi:Host"]);
            uriBuilder.Path = $"{_configuration["AutoRiaApi:AutoTypesPath"]}/{categoryId}/marks/{makeId}/models";

            var stringBuilder = new StringBuilder();
            uriBuilder.Query = stringBuilder.AppendJoin("&", carsParameters.Select(p => $"{p.Key}={p.Value}")).ToString();

            var response = await _httpClient.GetAsync(uriBuilder.Uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}