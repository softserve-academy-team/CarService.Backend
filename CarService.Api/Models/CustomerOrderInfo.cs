using System.Collections.Generic;
using CarService.Api.Models.DTO;

namespace CarService.Api.Models
{
    public class CustomerOrderInfo
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public int AutoRiaId { get; set; }
        public string MarkName { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string PhotoLink { get; set; }
        public IEnumerable<ReviewPropositionDto> ReviewPropositions { get; set; }
        public int MechanicId { get; set; }
        public int ReviewId { get; set; }
    }
}