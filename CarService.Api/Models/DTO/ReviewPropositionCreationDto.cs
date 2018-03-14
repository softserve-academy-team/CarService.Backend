using System;
using System.ComponentModel.DataAnnotations;
using CarService.DbAccess.Entities;

namespace CarService.Api.Models.DTO
{
    public class ReviewPropositionCreationDto
    {
        [Required]
        public int OrderId { get; set; }
        public string ReviewComment { get; set; }

        [Required]
        [Range(0, 100000)]
        public int ReviewPrice { get; set; }

    }
}