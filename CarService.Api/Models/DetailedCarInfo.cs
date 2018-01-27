namespace CarService.Api.Models
{
    public class DetailedCarInfo : BaseCarInfo
    {
        public string Description { get; set; }
        public string FuelName { get; set; }
        public string GearBoxName { get; set; }
    }
}
