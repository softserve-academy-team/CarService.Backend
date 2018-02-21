namespace CarService.Api.Models
{
    public class RegisterMechanicCredentials : RegisterCustomerCredentials
    {
        public int Experience { get; set; }
        public string Specialization { get; set; }
    }
}