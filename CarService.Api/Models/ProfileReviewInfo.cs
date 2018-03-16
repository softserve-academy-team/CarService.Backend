namespace CarService.Api.Models
{
    public class ProfileReviewInfo
    {
        public int ReviewId {get; set; }
        public string Date { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
        public string City { get; set; }
    }
}