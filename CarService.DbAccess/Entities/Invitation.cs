using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System;

namespace CarService.DataAccess.Model
{
    public class Invitation : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}