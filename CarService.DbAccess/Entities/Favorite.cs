using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }
        public int AutoRiaId { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
