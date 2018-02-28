using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class CustomerAuto : IEntity
    {
        public int Id { get; set; }
        
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? AutoId { get; set; }
        public Auto Auto { get; set; }
    }
}
