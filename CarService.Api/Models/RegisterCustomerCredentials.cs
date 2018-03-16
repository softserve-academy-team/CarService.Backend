using System.ComponentModel.DataAnnotations;

namespace CarService.Api.Models
{
    public class RegisterCustomerCredentials
    {
        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Password { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Location { get; set; }
    }
}