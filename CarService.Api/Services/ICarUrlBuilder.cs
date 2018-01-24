using System.Collections.Generic;

namespace CarService.Api.Services
{
    public interface ICarUrlBuilder
    {
        string Build(string baseUrl, IDictionary<string, string> carParameters);
    }
}