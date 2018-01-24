using System.Text;
using System.Collections.Generic;

namespace CarService.Api.Services
{
    public class AutoRiaCarUrlBuilder : ICarUrlBuilder
    {
        public string Build(string baseUrl, IDictionary<string, string> carParameters)
        {
            var urlBuilder = new StringBuilder(baseUrl);
            urlBuilder.Append("?");
            foreach (KeyValuePair<string, string> item in carParameters)
            {
                urlBuilder.Append($"{item.Key}={item.Value}&");
            }
            urlBuilder.Remove(urlBuilder.Length - 1, 1);
            return urlBuilder.ToString();
        }
    }
}