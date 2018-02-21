﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class ReviewProposition : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public int Price { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public string MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
    }
}
