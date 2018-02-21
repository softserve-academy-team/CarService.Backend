using System;
using System.ComponentModel.DataAnnotations;

namespace CarService.DbAccess.Entities
{
    public class Transaction : IEntity
    {
        [Key]
        public int EntityId { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
