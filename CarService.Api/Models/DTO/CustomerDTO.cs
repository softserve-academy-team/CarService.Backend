namespace CarService.Api.Models.DTO
{
    public class CustomerDTO : UserDTO
    {
        public string City { get; set; }
        public string CardNumber { get; set; }
    }
}