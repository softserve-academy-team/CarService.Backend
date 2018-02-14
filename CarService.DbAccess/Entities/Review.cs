using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Review : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public int AutoRate { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
        public string Videos { get; set; }
        public DateTime Date { get; set; }

        public string MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}