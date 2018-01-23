using System;

namespace CarService.DbAccess.Entities
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; }

        //TODO:Check Sender:User_Id, Reciever:User_Id
        public int Sender { get; set; }
        public int Reciever { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
