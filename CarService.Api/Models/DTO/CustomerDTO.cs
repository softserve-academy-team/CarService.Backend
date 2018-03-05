using System.ComponentModel.DataAnnotations;

namespace CarService.Api.Models.DTO
{
    public class CustomerDTO : UserDTO
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string City { get; set; }
        [RegularExpression(@"[0-9]{16}")]
        [MaxLength(16)]
        public string CardNumber { get; set; }
    }
}