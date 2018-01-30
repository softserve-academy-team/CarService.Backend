using System;

namespace CarService.DbAccess.Entities
{
    public class ReviewProposition : IEntity
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
    }
}
