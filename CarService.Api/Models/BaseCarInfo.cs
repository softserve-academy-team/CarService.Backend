namespace CarService.Api.Models
{
    public class BaseCarInfo
    {
        public int AutoId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
        public int PriceUSD { get; set; }
        public int PriceUAH { get; set; }
        public int PriceEUR { get; set; }
        public string Race { get; set; }
        public int RaceInt { get; set; }
        public string City { get; set; }
    }
}