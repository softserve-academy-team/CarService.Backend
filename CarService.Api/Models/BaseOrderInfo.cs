namespace CarService.Api.Models
{
    public class BaseOrderInfo
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhotoUrl { get; set; }
        public int AutoId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public string CarPhotoUrl { get; set; }
    }
}