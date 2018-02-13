using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.DbAccess.Entities
{
    public class User : IdentityUser
    {
        // public int Id { get; set; }
        // public string IdIdentity { get; set; }
        // public string Email { get; set; }
        // public string Password { get; set; }
        public UserStatus Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public ICollection<Transaction> SendersTransactions { get; set; }
        public ICollection<Transaction> ReceiversTransactions { get; set; }

        public User()
        {
            SendersTransactions = new Collection<Transaction>();
            ReceiversTransactions = new Collection<Transaction>();
        }
    }
}
