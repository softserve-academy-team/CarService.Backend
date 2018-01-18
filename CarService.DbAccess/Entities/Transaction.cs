using CarService.DataAccess.Model;
using CarService.DbAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.DataAccess.Model
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
