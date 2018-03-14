using System.ComponentModel.DataAnnotations;

namespace CarService.Api.Models.DTO
{
    public class UserDto
    {
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string LastName { get; set; }
        [RegularExpression(@"\+38[0-9]{10}")]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
    }
}