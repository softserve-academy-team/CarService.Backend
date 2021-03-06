﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }
    }
}