using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}