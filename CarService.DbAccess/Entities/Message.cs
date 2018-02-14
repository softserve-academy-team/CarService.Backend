using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Message : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int? DialogId { get; set; }
        public Dialog Dialog { get; set; }
    }
}