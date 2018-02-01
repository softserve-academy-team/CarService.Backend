using CarService.Api.Models;

namespace CarService.Tests.Extensions
{
    public static class BaseCarInfoValidatorExtensions
    {
        public static bool IsValid(this BaseCarInfo baseCarInfo)
        {
            return baseCarInfo.AutoId > 0
                && !string.IsNullOrWhiteSpace(baseCarInfo.MarkName)
                && !string.IsNullOrWhiteSpace(baseCarInfo.ModelName)
                && baseCarInfo.Year > 0
                && !string.IsNullOrWhiteSpace(baseCarInfo.PhotoLink)
                && baseCarInfo.PriceUSD >= 0
                && baseCarInfo.PriceUAH >= 0
                && baseCarInfo.PriceEUR >= 0
                && !string.IsNullOrWhiteSpace(baseCarInfo.Race)
                && baseCarInfo.RaceInt >= 0
                && !string.IsNullOrWhiteSpace(baseCarInfo.City)
                && !string.IsNullOrWhiteSpace(baseCarInfo.FuelName)
                && !string.IsNullOrWhiteSpace(baseCarInfo.GearBoxName);
        }
    }
}