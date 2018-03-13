namespace CarService.Api.Models.DTO
{
    public class ReviewPropositionDto
    {
        public int Id {get; set; }
        public int MechanicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double MechanicRate { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public string Date { get; set; }
    }
}