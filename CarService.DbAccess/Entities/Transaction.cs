using System;

namespace CarService.DbAccess.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }
        
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
