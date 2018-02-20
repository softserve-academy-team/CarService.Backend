namespace CarService.Api.Models.DTO
{
    public class MechanicDTO : CustomerDTO
    {
        public string WorkExperience { get; set; }
        public int MechanicRate { get; set; }
    }
}