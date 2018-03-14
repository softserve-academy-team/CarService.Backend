namespace CarService.Api.Models
{
    public class MechanicOrderInfo
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int AutoRiaId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public string PhotoLink { get; set; }
        public int PropositionPrice { get; set; }
        public string PropositionComment { get; set; }
        public string PropositionDate { get; set; }
        public int ReviewId { get; set; }
        public bool IsDoIt { get; set; }
    }
}