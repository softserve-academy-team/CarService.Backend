using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DataAccess.Model
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public int AutoRate { get; set; }

        public string Description { get; set; }

        public string Photos { get; set; }

        public string Videos { get; set; }

        public DateTime Date{ get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}