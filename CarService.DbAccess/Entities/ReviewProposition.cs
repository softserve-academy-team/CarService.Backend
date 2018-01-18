using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Model
{
    public class ReviewProposition : IEntity
    {
        public int Id { get; set; }
        public int Price { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
    }
}
