using System;
using CarService.DbAccess.Entities;

namespace CarService.Api.Models.DTO
{
    public class OrderCreationDto
    {
        public int AutoRiaId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int MarkId { get; set; }
        public int ModelId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
        public string City { get; set; }
    }
}