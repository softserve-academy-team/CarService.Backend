using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Invitation : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}