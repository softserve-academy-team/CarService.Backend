namespace CarService.Api.Models.DTO
{
    public class MechanicDTO : CustomerDTO
    {
        public int WorkExperience { get; set; }
        public string Specialization { get; set; }
        public int MechanicRate { get; set; }
    }
}