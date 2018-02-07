namespace CarService.Api.Models
{
    public class RegisterMechanicCredentials : RegisterCustomerCredentials
    {
        public int CardNumber { get; set;}
        public string WorkExperience { get; set; }
    }
}