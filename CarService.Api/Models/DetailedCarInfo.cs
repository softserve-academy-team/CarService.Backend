namespace CarService.Api.Models
{
    public class DetailedCarInfo : BaseCarInfo
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int MarkId { get; set; }
        public int ModelId { get; set; }
    }
}
