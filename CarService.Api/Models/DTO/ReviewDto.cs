using System.Collections.Generic;

namespace CarService.Api.Models.DTO
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public IEnumerable<string> Photos { get; set; }
        public IEnumerable<string> Videos { get; set; }
        public int AutoRiaId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public string PhotoLink { get; set; }
    }
}