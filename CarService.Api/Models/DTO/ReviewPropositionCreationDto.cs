using System;
using CarService.DbAccess.Entities;

namespace CarService.Api.Models.DTO
{
    public class ReviewPropositionCreationDto
    {
        public int OrderId { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewPrice { get; set; }

    }
}