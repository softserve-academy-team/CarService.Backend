using System;

namespace CarService.DbAccess.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int? SenderId { get; set; }
        public User Sender { get; set; }
        
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
