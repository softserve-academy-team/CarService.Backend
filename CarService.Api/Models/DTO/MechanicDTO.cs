using System.ComponentModel.DataAnnotations;

namespace CarService.Api.Models.DTO
{
    public class MechanicDto : CustomerDto
    {
        [Required]
        [Range(0, 60)]
        public int WorkExperience { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(40)]
        public string Specialization { get; set; }
        public double MechanicRate { get; set; }
    }
}