using System.ComponentModel.DataAnnotations;

namespace CarService.Api.Models
{
    public class RegisterMechanicCredentials : RegisterCustomerCredentials
    {
        [Required]
        [Range(0, 60)]
        public int Experience { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Specialization { get; set; }
    }
}