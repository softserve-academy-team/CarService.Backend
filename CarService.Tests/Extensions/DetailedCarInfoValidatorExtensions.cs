using CarService.Api.Models;

namespace CarService.Tests.Extensions
{
    public static class DetailedCarInfoValidatorExtensions
    {
        public static bool IsValid(this DetailedCarInfo detailedCarInfo)
        {
            return (detailedCarInfo as BaseCarInfo).IsValid()
                && !string.IsNullOrWhiteSpace(detailedCarInfo.Description);
        }
    }
}