namespace CarService.Api.Models
{
    public class OrderSearchModel
    {
        public int TypeId { get; set; }
        public int MarkId { get; set; }
        public int ModelId { get; set; }
        public string City { get; set; }
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
    }
}
